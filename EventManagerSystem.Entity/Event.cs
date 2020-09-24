using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagerSystem.Entity
{
    public class Event : BaseEntity
    {
        public string ImgURL { get; set; }
        public string Title { get; set; }
        public DateTime EventDate { get; set; }
        public string EventPlace { get; set; }
        public string Organizer { get; set; }
        public string Description { get; set; } 
    }
}
