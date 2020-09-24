using EventManagerSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagerSystem.Web.Models.EventsViewModels
{
    public class EventsDeleteViewModel : BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Required]
        public string ImgURL { get; set; }
        public string Title { get; set; }
        public DateTime EventDate { get; set; }
        public string EventPlace { get; set; }
        public string Organizer { get; set; }
        public string Description { get; set; }
    }
}