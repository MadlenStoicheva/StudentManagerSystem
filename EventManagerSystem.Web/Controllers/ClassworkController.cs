using EventManagerSystem.Data.Entity.Repositories;
using EventManagerSystem.Entity;
using EventManagerSystem.Web.Filters;
using EventManagerSystem.Web.Models.ClassworkViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventManagerSystem.Web.Controllers
{
    public class ClassworkController : Controller
    {

        static UserRepository userRepository = new UserRepository();
        User user = userRepository.GetById(LoginFilter.GetUserId());

        // GET: Events
        public ActionResult Index()
        {

            ClassworkRepository repository = new ClassworkRepository();
            List<Classwork> classworks = repository.GetAll();
            classworks.Reverse();

            ClassworkListViewModel model = new ClassworkListViewModel();
            model.Classworks = classworks;

            return View(model);
        }

        [AuthenticationFilter(RequireAdminRole = false)]
        public ActionResult Create()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Create(CRUDClassworkViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Classwork classwork = new Classwork();
            // events.Id = model.Id;
            classwork.Title = model.Title;
            classwork.Content = model.Content;
            classwork.StudentId = user.Id;



            var repository = new ClassworkRepository();
            repository.Insert(classwork);

            return RedirectToAction("Index");
        }

        [AuthenticationFilter(RequireAdminRole = false)]
        [System.Web.Mvc.HttpGet]
        public ActionResult Edit(int? id)
        {

            ClassworkRepository repository = new ClassworkRepository();

            CRUDClassworkViewModel model = new CRUDClassworkViewModel();

            if (id.HasValue)
            {
                Classwork classwork = repository.GetById(id.Value);
                model.Id = classwork.Id;
                model.Title = classwork.Title;
                model.Content = classwork.Content;
                model.StudentId = user.Id;

            }

            return View(model);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(CRUDClassworkViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ClassworkRepository repository = new ClassworkRepository();

            Classwork classwork = new Classwork();
            classwork.Id = model.Id;


            classwork.Title = model.Title;
            classwork.Content = model.Content;
            classwork.StudentId = user.Id;

            repository.Save(classwork);

            return RedirectToAction("Index");
        }

        [AuthenticationFilter(RequireAdminRole = false)]
        [System.Web.Mvc.HttpGet]
        public ActionResult Delete(int id)
        {

            ClassworkRepository repository = new ClassworkRepository();

            Classwork classwork = repository.GetById(id);

            CRUDClassworkViewModel model = new CRUDClassworkViewModel();
            model.Title = classwork.Title;
            model.Content = classwork.Content;
            model.StudentId = classwork.StudentId;


            return View(model);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Delete(CRUDClassworkViewModel model)
        {

            ClassworkRepository repository = new ClassworkRepository();
            if (model.Id.ToString() != String.Empty)
            {
                repository.Delete(model.Id);
            }


            return RedirectToAction("Index");
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Show(int id)
        {

            ClassworkRepository repository = new ClassworkRepository();

            Classwork classwork = repository.GetById(id);

            CRUDClassworkViewModel model = new CRUDClassworkViewModel();
            model.Title = classwork.Title;
            model.Content = classwork.Content;


            return View(model);
        }

        public ActionResult GetAllHomeworks()
        {
            return View();
        }

        public JsonResult GetHomeworks()
        {
            ClassworkRepository repository = new ClassworkRepository();

            List<Classwork> groups = repository.GetAll();

            return new JsonResult { Data = groups, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}