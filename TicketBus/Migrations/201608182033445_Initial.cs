namespace TicketBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BusStops",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Status = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Voyages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartureDateTime = c.DateTime(nullable: false),
                        ArrivalDateTime = c.DateTime(nullable: false),
                        TravelTime = c.Time(nullable: false, precision: 7),
                        VoyageNumber = c.Int(nullable: false),
                        VoyageName = c.String(nullable: false),
                        NumberOfSeats = c.Int(nullable: false),
                        OneTicketCost = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VoyageId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Voyages", t => t.VoyageId, cascadeDelete: true)
                .Index(t => t.VoyageId);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        PassengersFullName = c.String(nullable: false),
                        PassengersDocNumber = c.String(nullable: false),
                        PassengerSeatNumber = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
           
            
            CreateTable(
                "dbo.VoyageBusStops",
                c => new
                    {
                        Voyage_Id = c.Int(nullable: false),
                        BusStop_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Voyage_Id, t.BusStop_Id })
                .ForeignKey("dbo.Voyages", t => t.Voyage_Id, cascadeDelete: true)
                .ForeignKey("dbo.BusStops", t => t.BusStop_Id, cascadeDelete: true)
                .Index(t => t.Voyage_Id)
                .Index(t => t.BusStop_Id);
            
        }
        
        public override void Down()
        {

            DropForeignKey("dbo.Orders", "VoyageId", "dbo.Voyages");
            DropForeignKey("dbo.Tickets", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.VoyageBusStops", "BusStop_Id", "dbo.BusStops");
            DropForeignKey("dbo.VoyageBusStops", "Voyage_Id", "dbo.Voyages");
            DropIndex("dbo.VoyageBusStops", new[] { "BusStop_Id" });
            DropIndex("dbo.VoyageBusStops", new[] { "Voyage_Id" });

            DropIndex("dbo.Tickets", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "VoyageId" });
            DropTable("dbo.VoyageBusStops");

            DropTable("dbo.Tickets");
            DropTable("dbo.Orders");
            DropTable("dbo.Voyages");
            DropTable("dbo.BusStops");
        }
    }
}
