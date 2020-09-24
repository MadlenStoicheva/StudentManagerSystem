using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagerSystem.Entity
{
    public class Student : User
    {
        public int FacultyNumber { get; set; }
        public string Specialty { get; set; }

        public string GroupId { get; set; }

       // public List<Classwork> ClassworksList { get; set; }
      //  public List<Homework> HomeworksList { get; set; }
    }
}
