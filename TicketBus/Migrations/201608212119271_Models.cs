namespace TicketBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Models : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tickets", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "VoyageId", "dbo.Voyages");
            DropIndex("dbo.Orders", new[] { "VoyageId" });
            DropIndex("dbo.Tickets", new[] { "OrderId" });
            AddColumn("dbo.Tickets", "VoyageId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tickets", "VoyageId");
            AddForeignKey("dbo.Tickets", "VoyageId", "dbo.Voyages", "Id", cascadeDelete: true);
            DropColumn("dbo.BusStops", "Status");
            DropColumn("dbo.Tickets", "OrderId");
            DropColumn("dbo.Tickets", "Status");
            DropTable("dbo.Orders");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VoyageId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Tickets", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.Tickets", "OrderId", c => c.Int(nullable: false));
            AddColumn("dbo.BusStops", "Status", c => c.String(nullable: false));
            DropForeignKey("dbo.Tickets", "VoyageId", "dbo.Voyages");
            DropIndex("dbo.Tickets", new[] { "VoyageId" });
            DropColumn("dbo.Tickets", "VoyageId");
            CreateIndex("dbo.Tickets", "OrderId");
            CreateIndex("dbo.Orders", "VoyageId");
            AddForeignKey("dbo.Orders", "VoyageId", "dbo.Voyages", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Tickets", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
        }
    }
}
