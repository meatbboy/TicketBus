using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TicketBus.Models
{
    public class Voyage
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int DepartureBusStopId { get; set; }

        [Required]
        public int ArrivalBusStopId { get; set; }

        [Required]
        public DateTime DepartureDateTime { get; set; }

        [Required]
        public DateTime ArrivalDateTime { get; set; }

        [Required]
        [Column(TypeName = "time")]
        public TimeSpan TravelTime { get; set; }

        [Required]
        public int VoyageNumber { get; set; }

        [Required]
        public string VoyageName { get; set; }

        [Required]
        public int NumberOfSeats { get; set; }

        [Required]
        public int OneTicketCost { get; set; }
    }
}