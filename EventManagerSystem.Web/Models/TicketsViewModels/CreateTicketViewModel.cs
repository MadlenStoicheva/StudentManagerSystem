using EventManagerSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventManagerSystem.Web.Models.TicketsViewModels
{
    public class CreateTicketViewModel : BaseEntity
    {
        public int UserId { get; set; }
        public int EventId { get; set; }

        public List<SelectListItem> Users { get; set; }
        public List<SelectListItem> Events { get; set; }
    }
}