using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace TicketBus.Models
{
    public class BusStop
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string Status { get; set; }

        public virtual ICollection<Voyage> Voyages { get; set; } 
    }
}