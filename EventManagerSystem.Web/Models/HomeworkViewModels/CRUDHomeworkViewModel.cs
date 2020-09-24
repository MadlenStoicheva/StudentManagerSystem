using EventManagerSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagerSystem.Web.Models.HomeworkViewModels
{
    public class CRUDHomeworkViewModel : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int StudentId { get; set; }
    }
}