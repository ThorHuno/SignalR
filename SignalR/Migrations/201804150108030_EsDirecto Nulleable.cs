namespace SignalR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EsDirectoNulleable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Vuelos", "EsDirecto", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Vuelos", "EsDirecto", c => c.Boolean());
        }
    }
}
