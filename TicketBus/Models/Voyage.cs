using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TicketBus.Models
{
    public class Voyage
    {
        public int Id { get; set; }

        public int DepartureBusStopId { get; set; }

        public int ArrivalBusStopId { get; set; }

        public DateTime DepartureDateTime { get; set; }

        public DateTime ArrivalDateTime { get; set; }
       
        [Column(TypeName = "time")]
        public TimeSpan TravelTime { get; set; }

        public int VoyageNumber { get; set; }

        public string VoyageName { get; set; }

        public int NumberOfSeats { get; set; }

        public int OneTicketCost { get; set; }
    }
}