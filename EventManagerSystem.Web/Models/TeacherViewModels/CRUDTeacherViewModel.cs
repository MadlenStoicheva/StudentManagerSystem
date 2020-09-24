using EventManagerSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagerSystem.Web.Models.TeacherViewModels
{
    public class CRUDTeacherViewModel : User
    {
        public string Subject { get; set; }
    }
}