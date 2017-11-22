namespace Cana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsuariosAgregados : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 20),
                        Password = c.String(),
                        Token = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.UserName, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Usuarios", new[] { "UserName" });
            DropTable("dbo.Usuarios");
        }
    }
}
