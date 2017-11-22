namespace Cana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OracleDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "clientes.Condenas",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        FechaInicioCondena = c.DateTime(nullable: false),
                        FechaCondena = c.DateTime(nullable: false),
                        PresoID = c.Decimal(precision: 10, scale: 0),
                        JuezID = c.Decimal(precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("clientes.Juezes", t => t.JuezID)
                .ForeignKey("clientes.Presos", t => t.PresoID)
                .Index(t => t.PresoID)
                .Index(t => t.JuezID);
            
            CreateTable(
                "clientes.Delitos",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Nombre = c.String(maxLength: 50),
                        CondenaMinima = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CondenaMaxima = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "clientes.Juezes",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Nombre = c.String(maxLength: 50),
                        Rut = c.String(maxLength: 50),
                        Sexo = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Domicilio = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "clientes.Presos",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Rut = c.String(maxLength: 50),
                        Nombre = c.String(maxLength: 50),
                        Apellido = c.String(maxLength: 50),
                        FechaNacimiento = c.DateTime(nullable: false),
                        Domicilio = c.String(maxLength: 50),
                        Sexo = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "clientes.Usuarios",
                c => new
                    {
                        ID = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        UserName = c.String(maxLength: 20),
                        Password = c.String(maxLength: 50),
                        Token = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.UserName, unique: true);
            
            CreateTable(
                "clientes.DelitoCondenas",
                c => new
                    {
                        Delito_ID = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Condena_ID = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => new { t.Delito_ID, t.Condena_ID })
                .ForeignKey("clientes.Delitos", t => t.Delito_ID, cascadeDelete: true)
                .ForeignKey("clientes.Condenas", t => t.Condena_ID, cascadeDelete: true)
                .Index(t => t.Delito_ID)
                .Index(t => t.Condena_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("clientes.Condenas", "PresoID", "clientes.Presos");
            DropForeignKey("clientes.Condenas", "JuezID", "clientes.Juezes");
            DropForeignKey("clientes.DelitoCondenas", "Condena_ID", "clientes.Condenas");
            DropForeignKey("clientes.DelitoCondenas", "Delito_ID", "clientes.Delitos");
            DropIndex("clientes.DelitoCondenas", new[] { "Condena_ID" });
            DropIndex("clientes.DelitoCondenas", new[] { "Delito_ID" });
            DropIndex("clientes.Usuarios", new[] { "UserName" });
            DropIndex("clientes.Condenas", new[] { "JuezID" });
            DropIndex("clientes.Condenas", new[] { "PresoID" });
            DropTable("clientes.DelitoCondenas");
            DropTable("clientes.Usuarios");
            DropTable("clientes.Presos");
            DropTable("clientes.Juezes");
            DropTable("clientes.Delitos");
            DropTable("clientes.Condenas");
        }
    }
}
