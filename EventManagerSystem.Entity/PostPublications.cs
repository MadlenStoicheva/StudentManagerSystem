using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagerSystem.Entity
{
    public class PostPublications : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImgURL { get; set; }

        public string Teacher { get; set; }
        //public Teacher Teacher { get; set; }
    }
}
