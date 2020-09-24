using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagerSystem.Entity
{
    public class WeeklyClasses : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public string ImgURL { get; set; }

        public string FilesURL { get; set; }
        public string MeetingURL { get; set; }
        public string Teacher { get; set; }
        // public Teacher TeacherId { get; set; }

    }
}
