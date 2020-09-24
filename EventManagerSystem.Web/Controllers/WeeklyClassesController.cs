using EventManagerSystem.Data.Entity.Repositories;
using EventManagerSystem.Entity;
using EventManagerSystem.Web.Filters;
using EventManagerSystem.Web.Models.PostPublicationsViewModels;
using EventManagerSystem.Web.Models.WeeklyClassesViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventManagerSystem.Web.Controllers
{
    public class WeeklyClassesController : Controller
    {
        // GET: Events
        public ActionResult Index()
        {
            WeeklyClassesRepository repository = new WeeklyClassesRepository();
            List<WeeklyClasses> weeklyClasses = repository.GetAll();
            weeklyClasses.Reverse();

            WeeklyclassesListViewModel model = new WeeklyclassesListViewModel();
            model.WeeklyClasses = weeklyClasses;

            return View(model);
        }

     //   [AuthenticationFilter(RequireAdminRole = true)]
        public ActionResult Create()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Create(CRUDWeeklyclassesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            WeeklyClasses weeklyClasses = new WeeklyClasses();

            weeklyClasses.Title = model.Title;
            weeklyClasses.Content = model.Content;
            weeklyClasses.Teacher = model.Teacher;
            weeklyClasses.ImgURL = model.ImgURL;
            weeklyClasses.FilesURL = model.FilesURL;
            weeklyClasses.MeetingURL = model.MeetingURL;
            // weeklyClasses.Teacher = model.Teacher;


            var repository = new WeeklyClassesRepository();
            repository.Insert(weeklyClasses);

            return RedirectToAction("Index");
        }

      ///  [AuthenticationFilter(RequireAdminRole = true)]
        [System.Web.Mvc.HttpGet]
        public ActionResult Edit(int? id)
        {

            WeeklyClassesRepository repository = new WeeklyClassesRepository();

            CRUDWeeklyclassesViewModel model = new CRUDWeeklyclassesViewModel();

            if (id.HasValue)
            {
                WeeklyClasses weeklyClasses = repository.GetById(id.Value);
                model.Id = weeklyClasses.Id;
                model.Title = weeklyClasses.Title;
                model.Content = weeklyClasses.Content;
                model.Teacher = weeklyClasses.Teacher;
                model.ImgURL = weeklyClasses.ImgURL;
                model.FilesURL = weeklyClasses.FilesURL;
                model.MeetingURL = weeklyClasses.MeetingURL;
                //  model.Teacher = weeklyClasses.Teacher;

            }

            return View(model);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(CRUDWeeklyclassesViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            WeeklyClassesRepository repository = new WeeklyClassesRepository();

            WeeklyClasses weeklyClasses = new WeeklyClasses();
            weeklyClasses.Id = model.Id;


            weeklyClasses.Title = model.Title;
            weeklyClasses.Content = model.Content;
            weeklyClasses.Teacher = model.Teacher;
            weeklyClasses.ImgURL = model.ImgURL;
            weeklyClasses.FilesURL = model.FilesURL;
            weeklyClasses.MeetingURL = model.MeetingURL;
            //  weeklyClasses.Teacher = model.Teacher;

            repository.Save(weeklyClasses);

            return RedirectToAction("Index");
        }

       // [AuthenticationFilter(RequireAdminRole = true)]
        [System.Web.Mvc.HttpGet]
        public ActionResult Delete(int id)
        {

            WeeklyClassesRepository repository = new WeeklyClassesRepository();

            WeeklyClasses weeklyClasses = repository.GetById(id);

            CRUDWeeklyclassesViewModel model = new CRUDWeeklyclassesViewModel();
            model.Title = weeklyClasses.Title;
            model.Content = weeklyClasses.Content;
            model.Teacher = weeklyClasses.Teacher;
            model.ImgURL = weeklyClasses.ImgURL;
            model.FilesURL = weeklyClasses.FilesURL;
            model.MeetingURL = weeklyClasses.MeetingURL;
            // model.Teacher = weeklyClasses.Teacher;


            return View(model);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Delete(CRUDWeeklyclassesViewModel model)
        {

            WeeklyClassesRepository repository = new WeeklyClassesRepository();
            if (model.Id.ToString() != String.Empty)
            {
                repository.Delete(model.Id);
            }


            return RedirectToAction("Index");
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Show(int id)
        {

            WeeklyClassesRepository repository = new WeeklyClassesRepository();

            WeeklyClasses weeklyClasses = repository.GetById(id);

            CRUDWeeklyclassesViewModel model = new CRUDWeeklyclassesViewModel();
            model.Title = weeklyClasses.Title;
            model.Content = weeklyClasses.Content;
            model.Teacher = weeklyClasses.Teacher;
            model.ImgURL = weeklyClasses.ImgURL;
            model.FilesURL = weeklyClasses.FilesURL;
            model.MeetingURL = weeklyClasses.MeetingURL;
            //  model.Teacher = weeklyClasses.Teacher;


            return View(model);
        }

        public ActionResult GetAllGroups()
        {
            return View();
        }

        public JsonResult GetGroups()
        {
            WeeklyClassesRepository repository = new WeeklyClassesRepository();

            List<WeeklyClasses> classes = repository.GetAll();

            return new JsonResult { Data = classes, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}