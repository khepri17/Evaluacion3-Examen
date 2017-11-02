using Cana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cana.Controllers
{
    public class PresosController : ApiController
    {
        private CanaDBContext context;

        public PresosController()
        {
            this.context = new CanaDBContext();
        }

        //metodo listar todos los presos
        //retorna lista de presos y su lista de condenas
        public IEnumerable<Object> get()
        {
            return context.Presos.Select(c => new
            {
                ID = c.ID,
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                Sexo = c.Sexo,
                FechaNacimiento = c.FechaNacimiento,
                Domicilio = c.Domicilio,
                Condenas = c.Condenas.Select(g => new
                {
                    ID = g.ID,
                    FechaInicio = g.FechaCondena,
                    FechaCondena = g.FechaCondena,
                    juez = g.Juez.Nombre,
                    delitos = g.Delitos.Select(d => new
                    {
                        ID = d.ID,
                        Nombre = d.Nombre
                    })
                })
            });
        }

        //metodo buscar preso
        //retorna solo un preso
        public IHttpActionResult get(int id)
        {
            Preso preso = context.Presos.Find(id);
            if (preso == null)
            {
                return NotFound();//404
            }
            return Ok(preso);//retorno codigo 200 + preso buscado
        }

        //metodo agregar preso
        public IHttpActionResult post(Preso preso)
        {
            context.Presos.Add(preso);
            int filasAfectadas = context.SaveChanges();

            if (filasAfectadas == 0)
            {
                return InternalServerError(); //error 500
            }

            return Ok(new { Mensaje = "Preso agregado de forma correcta" });
        }

        //metodo eliminar preso
        public IHttpActionResult delete(int id)
        {
            //buscar preso a eliminar
            Preso preso = context.Presos.Find(id);
            if (preso == null) return NotFound();//404

            context.Presos.Remove(preso);
            if (context.SaveChanges() > 0)
            {
                //retorno codigo 200
                return Ok(new { Mensaje = "Preso eliminado de forma correcta" });
            }
            return InternalServerError();//500
        }

        //metodo para modificar preso
        public IHttpActionResult put(Preso preso)
        {
            context.Entry(preso).State = System.Data.Entity.EntityState.Modified;
            if (context.SaveChanges() > 0)
            {
                return Ok(new { Mensaje = "Preso se modifico de forma correcta" });
            }
            return InternalServerError();
        }
    }
}
