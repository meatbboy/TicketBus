namespace TicketBus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Models : DbMigration
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
                        DepartureBusStopId = c.Int(nullable: false),
                        ArrivalBusStopId = c.Int(nullable: false),
                        DepartureDateTime = c.DateTime(nullable: false),
                        ArrivalDateTime = c.DateTime(nullable: false),
                        TravelTime = c.Time(nullable: false, precision: 7),
                        VoyageNumber = c.Int(nullable: false),
                        VoyageName = c.String(nullable: false),
                        NumberOfSeats = c.Int(nullable: false),
                        OneTicketCost = c.Int(nullable: false),
                        BusStop_Id = c.Int(),
                        BusStop_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BusStops", t => t.ArrivalBusStopId, cascadeDelete: false)
                .ForeignKey("dbo.BusStops", t => t.DepartureBusStopId, cascadeDelete: false)
                .ForeignKey("dbo.BusStops", t => t.BusStop_Id)
                .ForeignKey("dbo.BusStops", t => t.BusStop_Id1)
                .Index(t => t.DepartureBusStopId)
                .Index(t => t.ArrivalBusStopId)
                .Index(t => t.BusStop_Id)
                .Index(t => t.BusStop_Id1);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Voyages", "BusStop_Id1", "dbo.BusStops");
            DropForeignKey("dbo.Voyages", "BusStop_Id", "dbo.BusStops");
            DropForeignKey("dbo.Orders", "VoyageId", "dbo.Voyages");
            DropForeignKey("dbo.Tickets", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Voyages", "DepartureBusStopId", "dbo.BusStops");
            DropForeignKey("dbo.Voyages", "ArrivalBusStopId", "dbo.BusStops");
            DropIndex("dbo.Tickets", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "VoyageId" });
            DropIndex("dbo.Voyages", new[] { "BusStop_Id1" });
            DropIndex("dbo.Voyages", new[] { "BusStop_Id" });
            DropIndex("dbo.Voyages", new[] { "ArrivalBusStopId" });
            DropIndex("dbo.Voyages", new[] { "DepartureBusStopId" });
            DropTable("dbo.Tickets");
            DropTable("dbo.Orders");
            DropTable("dbo.Voyages");
            DropTable("dbo.BusStops");
        }
    }
}
