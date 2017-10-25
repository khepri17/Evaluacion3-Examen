namespace Cana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigracionPrimaria : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Condenas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FechaInicioCondena = c.DateTime(nullable: false),
                        FechaCondena = c.DateTime(nullable: false),
                        PresoID = c.Int(),
                        JuezID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Juezes", t => t.JuezID)
                .ForeignKey("dbo.Presos", t => t.PresoID)
                .Index(t => t.PresoID)
                .Index(t => t.JuezID);
            
            CreateTable(
                "dbo.Delitos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        CondenaMinima = c.Int(nullable: false),
                        CondenaMaxima = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Juezes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Rut = c.String(),
                        Sexo = c.Int(nullable: false),
                        Domicilio = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Presos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Rut = c.String(),
                        Nombre = c.String(),
                        Apellido = c.String(),
                        FechaNacimiento = c.DateTime(nullable: false),
                        Domicilio = c.String(),
                        Sexo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.DelitoCondenas",
                c => new
                    {
                        Delito_ID = c.Int(nullable: false),
                        Condena_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Delito_ID, t.Condena_ID })
                .ForeignKey("dbo.Delitos", t => t.Delito_ID, cascadeDelete: true)
                .ForeignKey("dbo.Condenas", t => t.Condena_ID, cascadeDelete: true)
                .Index(t => t.Delito_ID)
                .Index(t => t.Condena_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Condenas", "PresoID", "dbo.Presos");
            DropForeignKey("dbo.Condenas", "JuezID", "dbo.Juezes");
            DropForeignKey("dbo.DelitoCondenas", "Condena_ID", "dbo.Condenas");
            DropForeignKey("dbo.DelitoCondenas", "Delito_ID", "dbo.Delitos");
            DropIndex("dbo.DelitoCondenas", new[] { "Condena_ID" });
            DropIndex("dbo.DelitoCondenas", new[] { "Delito_ID" });
            DropIndex("dbo.Condenas", new[] { "JuezID" });
            DropIndex("dbo.Condenas", new[] { "PresoID" });
            DropTable("dbo.DelitoCondenas");
            DropTable("dbo.Presos");
            DropTable("dbo.Juezes");
            DropTable("dbo.Delitos");
            DropTable("dbo.Condenas");
        }
    }
}
