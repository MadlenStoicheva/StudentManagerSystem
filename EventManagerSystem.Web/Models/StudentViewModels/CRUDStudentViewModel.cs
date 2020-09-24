using EventManagerSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagerSystem.Web.Models.StudentViewModels
{
    public class CRUDStudentViewModel : User
    {
        public int FacultyNumber { get; set; }
        public string Specialty { get; set; }
        public string GroupId { get; set; }
    }
}