using Cana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Oracle.ManagedDataAccess.Client;

namespace Cana.Controllers
{
    [AuthenticationFilter]
    public class JuezesController : ApiController
    {
        private CanaDBContext context;

        public JuezesController()
        {
            this.context = new CanaDBContext();
        }

        //metodo listar todos los juezes
        //retornar lista de juezes
        public IEnumerable<Object> get()
        {
            return context.Juezes.Select(c => new
            {
                ID = c.ID,
                Nombre = c.Nombre,
                Rut = c.Rut,
                Sexo = c.Sexo,
                Domicilio = c.Domicilio,
                Condenas = c.Condenas.Select(g => new
                {
                    ID = g.ID,
                    FechaInicio = g.FechaInicioCondena,
                    preso = new
                    {
                        NombrePreso = g.Preso.Nombre
                    }
                })
            });

        }
        //metodo buscar juez
        // retornar solo un juez 
        public IHttpActionResult get(int id)
        {
            

            try
            {
                OracleParameter param1 = new OracleParameter("id_juez", id);
                OracleParameter param2 = new OracleParameter("listado",
                    OracleDbType.RefCursor, System.Data.ParameterDirection.Output);

                Juez juez =
                    context.
                        Database.
                        SqlQuery<Juez>("begin sp_buscar_juez(:id_juez, :listado); end;",
                        param1, param2).First();
                return Ok(juez);
            }
            catch (Exception)
            {

                return NotFound();
            }
        }
        //metodo agregar juez
        public IHttpActionResult post(Juez juez)
        {
            
            

            try
            {
                //declaracion de parametros
                OracleParameter param1 = new OracleParameter("nombre_juez", juez.Nombre);
                OracleParameter param2 = new OracleParameter("rut_juez", juez.Rut);
                OracleParameter param3 = new OracleParameter("sexo", juez.Sexo);
                OracleParameter param4 = new OracleParameter("direccion", juez.Domicilio);
                context.
                    Database.
                     ExecuteSqlCommand("begin sp_agregar_juez(:nombre_juez,:rut_juez ,:sexo ,:direccion); end;", param1, param2, param3, param4);

                return Ok(new{Mensaje = "Juez Agregado Correctamente" });
            }
            catch (Exception)
            {

                return InternalServerError();
            }

        }

        //metodo eliminar juez
        public IHttpActionResult delete(int id)
        {
            //buscar juez a eliminar
            Juez juez = context.Juezes.Find(id);
            if (juez == null) return NotFound();//404

            context.Juezes.Remove(juez);
            if (context.SaveChanges() > 0)
            {
                //retornar codigo 200
                return Ok(new { Mensaje = "Juez eliminado de forma correcta" });
            }
            return InternalServerError();//500
        }

        //metodo para modificar juez
        public IHttpActionResult put(Juez juez)
        {
            context.Entry(juez).State = System.Data.Entity.EntityState.Modified;
            if (context.SaveChanges() > 0)
            {
                return Ok(new { Mensaje = "Juez se modifico de forma correcta" });
            }
            return InternalServerError();
        }
    }
}
