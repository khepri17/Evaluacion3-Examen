namespace Cana.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Cana.Models.CanaDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Cana.Models.CanaDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Delitos.AddOrUpdate(x => x.ID,
                new Delito() { ID = 1, Nombre = "Homicidio", CondenaMinima = 5, CondenaMaxima = 50 },
                new Delito() { ID = 2, Nombre = "Femicidio", CondenaMinima = 5, CondenaMaxima = 50 },
                new Delito() { ID = 3, Nombre = "Robo con intimidacion", CondenaMinima = 1, CondenaMaxima = 12 },
                new Delito() { ID = 4, Nombre = "Robo en lugar no habitado", CondenaMinima = 1, CondenaMaxima = 5 },
                new Delito() { ID = 5, Nombre = "Cohecho", CondenaMinima = 5, CondenaMaxima = 8 }

                );
            context.Juezes.AddOrUpdate(x => x.ID,
                new Juez() { ID = 1, Nombre = "Miguel", Domicilio = "Los Toros", Rut = "19035559-4", Sexo = 1 }
                );
            context.Presos.AddOrUpdate(x => x.ID,
                new Preso() { ID = 1, Nombre = "Alvaro", Apellido = "Catrivil", Rut = "13347875-4", Sexo = 1, FechaNacimiento = System.DateTime.Parse("10/12/1989 13:33"), Domicilio = "Ejemplo" }
            );

            context.Usuarios.AddOrUpdate(u => u.UserName,
                new Models.Usuario() { UserName = "admin", Password = "admin" },
                new Models.Usuario() { UserName = "miguel", Password = "1234" }
            );
        }
    }
}
