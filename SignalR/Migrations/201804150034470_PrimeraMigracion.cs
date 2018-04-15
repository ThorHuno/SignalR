namespace SignalR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrimeraMigracion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vuelos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Codigo = c.String(nullable: false, maxLength: 20, unicode: false),
                        Origen = c.String(nullable: false, maxLength: 30, unicode: false),
                        Destino = c.String(nullable: false, maxLength: 30, unicode: false),
                        EsDirecto = c.Boolean(),
                        HoraSalida = c.DateTime(nullable: false),
                        HoraLlegada = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Vuelos");
        }
    }
}
