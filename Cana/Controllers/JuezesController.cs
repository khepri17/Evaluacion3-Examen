using Cana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cana.Controllers
{
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
            Juez juez = context.Juezes.Find(id);
            if (juez == null)
            {
                return NotFound();//404
            }
            return Ok(juez);//retornamos codigo 200 + juez buscado
        }
        //metodo agregar juez
        public IHttpActionResult post(Juez juez)
        {
            context.Juezes.Add(juez);
            int filasAfectadas = context.SaveChanges();

            if (filasAfectadas == 0)
            {
                return InternalServerError();//error 500
            }

            return Ok(new { Mensaje = "Juez agregado de forma correcta" });
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
