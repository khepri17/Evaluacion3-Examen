using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Cana.Models
{
    public class CanaDBContext:DbContext
    {
        public DbSet<Condena> Condenas { get; set; }
        public DbSet<Delito> Delitos { get; set; }
        public DbSet<Juez> Juezes { get; set; }
        public DbSet<Preso> Presos { get; set; }
    }
}