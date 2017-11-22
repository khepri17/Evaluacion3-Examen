using Cana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace Cana.Controllers
{
    public class LoginController : ApiController
    {
        private CanaDBContext context;

        public LoginController()
        {
            this.context = new CanaDBContext();
        }

        public static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = MD5.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }


        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        public IHttpActionResult post(Login login)
        {
            try
            {

                Usuario user = context.Usuarios.Where(u => u.UserName == login.UserName && u.Password == login.Password).FirstOrDefault();

                //si el usuario no existe el login es incorrecto
                if (user == null)
                {
                    return Unauthorized();
                }

                //si el usuario existe verificamos si tiene un token

                if (user.Token != null)
                {
                    return Ok(new
                    {
                        Token = user.Token
                    });
                }

                //si el usuario no tiene un token lo generamos
                String token = GetHashString(user.UserName);


                //guardamos el token en la tabla usuario para no tener que volver a generarlo otra vez

                user.Token = token;
                context.Entry(user).State = System.Data.Entity.EntityState.Modified;
                if (context.SaveChanges() > 0)
                {
                    //si todo va bien y se puede guardar el token, éste se envia al usuario para que pueda almacenarlo en la aplicacion que se requiera
                    return Ok(new
                    {
                        Token = user.Token

                    });
                }
                return InternalServerError();
            }
            catch (Exception)
            {
                return InternalServerError();
            }

        }


    }
}
