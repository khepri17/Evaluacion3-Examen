using Cana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cana.Controllers
{
    public class CondenasController : ApiController
    {
        private CanaDBContext context;
        public CondenasController()
        {
            this.context = new CanaDBContext();
        }

        //metodo listar todas las condenas
        //retorna listado de condenas
        public IEnumerable<Object> get()
        {
            return context.Condenas.Select(c => new
            {
                ID = c.ID,
                FechaCondena = c.FechaCondena,
                FechaInicioCondena = c.FechaInicioCondena,
                Preso = new
                {
                    NombrePreso = c.Preso.Nombre
                },
                delitos = c.Delitos.Select(d => new
                {
                    ID = d.ID,
                    NombreDelito = d.Nombre
                })
            });
        }
        //metodo buscar condena
        //retorna solo una condena
        public IHttpActionResult get(int id)
        {
            Condena condena = context.Condenas.Find(id);
            if (condena == null)
            {
                return NotFound();//404
            }
            return Ok(condena); //retornamos codigo 200+ condena buscada
        }

        //metodo agregar condena¿'¿'¿'¿'
        public IHttpActionResult post(Condena condena)
        {
            context.Condenas.Add(condena);
            int filasAfectadas = context.SaveChanges();
            if (filasAfectadas == 0)
            {
                return InternalServerError();//error 500
            }
            return Ok(new { Mensaje = "Condena agregada de forma correcta" });
        }



        //metodo eliminar condena
        public IHttpActionResult delete(int id)
        {
            //buscar condena a eliminar
            Condena condena = context.Condenas.Find(id);
            if (condena == null) return NotFound();//404

            context.Condenas.Remove(condena);

            if (context.SaveChanges() > 0)
            {
                //retornar codigo 200
                return Ok(new { Mensaje = "Condena eliminada de forma correcta" });
            }
            return InternalServerError();//500
        }

        //metodo para modificar condena
        public IHttpActionResult put(Condena condena)
        {
            context.Entry(condena).State = System.Data.Entity.EntityState.Modified;

            if (context.SaveChanges() > 0)
            {
                return Ok(new { Mensaje = "Condena se modifico de forma correcta" });
            }
            return InternalServerError();
        }
    }
}
