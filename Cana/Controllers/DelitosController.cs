using Cana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cana.Controllers
{
    public class DelitosController : ApiController
    {
        private CanaDBContext context;


        public DelitosController()
        {
            this.context = new CanaDBContext();
        }

        //metodo listar todos los delitos
        //retorna lista de delitos
        public IEnumerable<Object> get()
        {
            return context.Delitos.Select(c => new
            {
                ID = c.ID,
                Nombre = c.Nombre,
                CondenaMinima = c.CondenaMinima,
                CondenaMaxima = c.CondenaMaxima
            });
        }

        //metodo buscar delito
        //retorna solo un delito
        public IHttpActionResult get(int id)
        {
            Delito delito = context.Delitos.Find(id);

            if (delito == null)
            {
                return NotFound();//404
            }
            return Ok(delito);//retornamos codigo 200 + delito buscado
        }

        //metodo agregar delito
        public IHttpActionResult post(Delito delito)
        {
            context.Delitos.Add(delito);

            int filasAfectadas = context.SaveChanges();

            if (filasAfectadas == 0)
            {
                return InternalServerError();//error 500
            }

            return Ok(new { Mensaje = "Delito agregado de forma correcta" });
        }

        //metodo eliminar delito
        public IHttpActionResult delete(int id)
        {
            //buscar delito a eliminar
            Delito delito = context.Delitos.Find(id);
            if (delito == null) return NotFound();//404

            context.Delitos.Remove(delito);

            if (context.SaveChanges() > 0)
            {
                //retorna codigo 200
                return Ok(new { Mensaje = "Delito eliminado de forma correcta" });
            }
            return InternalServerError();//500
        }

        //metodo para modificar delito
        public IHttpActionResult put(Delito delito)
        {
            context.Entry(delito).State = System.Data.Entity.EntityState.Modified;

            if (context.SaveChanges() > 0)
            {
                return Ok(new { Mensaje = "Delito se modifico de forma correcta" });
            }
            return InternalServerError();
        }
    }

}
