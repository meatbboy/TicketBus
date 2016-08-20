﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TicketBus.Models
{
    public class Ticket
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        [Required]
        [Display(Name = "Passenger's full name")]
        public string PassengersFullName { get; set; }

        [Required]
        [Display(Name = "Passenger's document number")]
        public string PassengersDocNumber { get; set; }

        [Required]
        [Display(Name = "Passenger's seat number")]
        public int PassengerSeatNumber { get; set; }

        [Required]
        public EnumStatus Status { get; set; }
    }
}