using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagerSystem.Entity
{
    public class Group : BaseEntity
    {
        public string Name { get; set; }
        public string Student { get; set; }
        public string Teacher { get; set; }

        //public virtual List<Student> StudentsList { get; set; }
      ////  public virtual Teacher Teacher { get; set; }
       // public virtual Student Student { get; set; }
    }
}
