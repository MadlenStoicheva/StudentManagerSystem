using EventManagerSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagerSystem.Web.Models.PostPublicationsViewModels
{
    public class CRUDPostPublicationsViewModel : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImgURL { get; set; }
        public string Teacher { get; set; }
        //  public Teacher Teacher { get; set; }
    }
}