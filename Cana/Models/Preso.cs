using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cana.Models
{
    [Table("Presos")]
    public class Preso
    {
        public int ID { get; set; }
        public string Rut { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Domicilio { get; set; }
        public int Sexo { get; set; }
        public List<Condena> Condenas { get; set; }
    }
}