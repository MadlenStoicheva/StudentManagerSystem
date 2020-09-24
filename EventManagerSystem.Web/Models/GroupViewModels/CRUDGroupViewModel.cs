using EventManagerSystem.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventManagerSystem.Web.Models.GroupViewModels
{
    public class CRUDGroupViewModel : BaseEntity
    {
        public string Name { get; set; }
        //public List<SelectListItem> Students { get; set; }
       // public List<SelectListItem> Teachers { get; set; }
        public string Student { get; set; }
        public string Teacher { get; set; }
    }
}