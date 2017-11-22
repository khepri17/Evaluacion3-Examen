using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cana.Models
{
    [Table("Delitos")]
    public class Delito
    {
        public int ID { get; set; }
        [MaxLength(50)]
        public string Nombre { get; set; }
        public int CondenaMinima { get; set; }
        public int CondenaMaxima { get; set; }
        public List<Condena> Condenas { get; set; }
    }
}