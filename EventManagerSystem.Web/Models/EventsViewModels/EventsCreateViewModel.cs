using EventManagerSystem.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventManagerSystem.Web.Models.EventsViewModels
{
    public class EventsCreateViewModel : BaseEntity
    {
        [Required(ErrorMessage = "Please input URL! It is required!")]
        public string ImgURL { get; set; }

        [Required(ErrorMessage = "Please input a title! It is required!")]
       // [StringLength(20, MinimumLength = 3, ErrorMessage = "Minimum length is 3 and maximum length is 20")]
       // [RegularExpression(@"^([A-z -]+)$", ErrorMessage = "Title can consist of letters, spaces and dashes only!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please input a event date! It is required!")]
        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        [Required(ErrorMessage = "Please input a place! It is required!")]
       // [StringLength(40, MinimumLength = 3, ErrorMessage = "Minimum length is 3 and maximum length is 40")]
       // [RegularExpression(@"^([A-z -]+)$", ErrorMessage = "EventPlace can consist of letters, spaces and dashes only!")]
        public string EventPlace { get; set; }

        [Required(ErrorMessage = "Please input a Organizer! It is required!")]
       // [StringLength(20, MinimumLength = 3, ErrorMessage = "Minimum length is 3 and maximum length is 20")]
       // [RegularExpression(@"^([A-z -]+)$", ErrorMessage = "Organizer can consist of letters, spaces and dashes only!")]
        public string Organizer { get; set; }


        [Required(ErrorMessage = "Please input a Organizer! It is required!")]
       // [StringLength(40, MinimumLength = 3, ErrorMessage = "Minimum length is 5 and maximum length is 40")]
      ///  [RegularExpression(@"^([A-z -]+)$", ErrorMessage = "Organizer can consist of letters, spaces and dashes only!")]
        public string Description { get; set; }
    }
}