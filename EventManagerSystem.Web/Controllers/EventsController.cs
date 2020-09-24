using EventManagerSystem.Data.Entity.Repositories;
using EventManagerSystem.Entity;
using EventManagerSystem.Web.Filters;
using EventManagerSystem.Web.Models;
using EventManagerSystem.Web.Models.EventsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventManagerSystem.Web.Controllers
{
    public class EventsController : Controller
    {
        // GET: Events
        public ActionResult Index()
        {
            EventRepository repository = new EventRepository();
            List<Event> events = repository.GetAll();
            events.Reverse();

            EventsListViewModel model = new EventsListViewModel();
            model.Events = events;

            return View(model);
        }

        [AuthenticationFilter(RequireAdminRole = true)]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(EventsCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Event events = new Event();
            // events.Id = model.Id;
            events.ImgURL = model.ImgURL;
            events.Title = model.Title;
            events.EventDate = model.EventDate;
            events.EventPlace = model.EventPlace;
            events.Organizer = model.Organizer;
            events.Description = model.Description;

            var repository = new EventRepository();
            repository.Insert(events);

            return RedirectToAction("Index");
        }

        [AuthenticationFilter(RequireAdminRole = true)]
        [HttpGet]
        public ActionResult Edit(int? id)
        {

            EventRepository repository = new EventRepository();

            EventsEditViewModel model = new EventsEditViewModel();

            if (id.HasValue)
            {
                Event events = repository.GetById(id.Value);
                model.Id = events.Id;
                model.ImgURL = events.ImgURL;
                model.Title = events.Title;
                model.EventDate = events.EventDate;
                model.EventPlace = events.EventPlace;
                model.Organizer = events.Organizer;
                model.Description = events.Description;

            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EventsEditViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            EventRepository repository = new EventRepository();

            Event events = new Event();
            events.Id = model.Id;
            events.ImgURL = model.ImgURL;
            events.Title = model.Title;

            if (events.EventDate != model.EventDate)
            {
                events.EventDate = model.EventDate;
            }
            else
            {
                events.EventDate = events.EventDate;
            }



            events.EventPlace = model.EventPlace;
            events.Organizer = model.Organizer;
            events.Description = model.Description;

            repository.Save(events);

            return RedirectToAction("Index");
        }

        [AuthenticationFilter(RequireAdminRole = true)]
        [HttpGet]
        public ActionResult Delete(int id)
        {

            EventRepository repository = new EventRepository();

            Event events = repository.GetById(id);

            EventsDeleteViewModel model = new EventsDeleteViewModel();
            model.ImgURL = events.ImgURL;
            model.Title = events.Title;
            model.EventDate = events.EventDate;
            model.EventPlace = events.EventPlace;
            model.Organizer = events.Organizer;
            model.Description = events.Description;

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(EventsDeleteViewModel model)
        {

            EventRepository repository = new EventRepository();
            if (model.Id.ToString() != String.Empty)
            {
                repository.Delete(model.Id);
            }


            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ShowEvent(int id)
        {

            EventRepository repository = new EventRepository();

            Event events = repository.GetById(id);

            EventsDeleteViewModel model = new EventsDeleteViewModel();
            model.ImgURL = events.ImgURL;
            model.Title = events.Title;
            model.EventDate = events.EventDate;
            model.EventPlace = events.EventPlace;
            model.Organizer = events.Organizer;
            model.Description = events.Description;

            return View(model);
        }

        public ActionResult GetAllEventsCalendar()
        {
            return View();
        }

        public JsonResult GetEvents()
        {
            EventRepository repository = new EventRepository();

            List<Event> events = repository.GetAll();
            
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}