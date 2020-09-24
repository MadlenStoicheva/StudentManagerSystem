using EventManagerSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagerSystem.Web.Models.TicketsViewModels
{
    public class DeleteTicketViewModel : BaseEntity
    {
        public int UserId { get; set; }
        public int EventId { get; set; }
    }
}