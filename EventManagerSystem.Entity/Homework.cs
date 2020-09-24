using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagerSystem.Entity
{
    public class Homework : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int StudentId { get; set; }
    }
}
