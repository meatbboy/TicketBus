using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TicketBus.Models
{
    public class Order
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int VoyageId { get; set; }

        public virtual Voyage Voyage { get; set; }

        [Required]
        public EnumStatus Status { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; } 
    }

    public enum EnumStatus
    {
        Reserved = 1,
        BoughtOut
    }
}