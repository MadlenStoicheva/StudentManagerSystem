using EventManagerSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagerSystem.Web.Models.WeeklyClassesViewModels
{
    public class CRUDWeeklyclassesViewModel : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImgURL { get; set; }

        public string FilesURL { get; set; }
        public string MeetingURL { get; set; }
        public string Teacher { get; set; }

        // public Teacher Teacher { get; set; }
    }
}