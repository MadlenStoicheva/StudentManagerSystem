using EventManagerSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagerSystem.Web.Models.TicketsViewModels
{
    public class TicketsListViewModel
    {
        public List<Ticket> Tickets { get; set; }
    }
}