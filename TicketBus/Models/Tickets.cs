using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TicketBus.Models
{
    public class Tickets
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public string PassengersFullName { get; set; }

        [Required]
        public string PassengersDocNumber { get; set; }

        [Required]
        public int PassengerSeatNumber { get; set; }

        [Required]
        public EnumStatus Status { get; set; }
    }
}