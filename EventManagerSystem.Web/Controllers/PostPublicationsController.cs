using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EventManagerSystem.Data.Entity;
using EventManagerSystem.Data.Entity.Repositories;
using EventManagerSystem.Entity;
using EventManagerSystem.Web.Filters;
using EventManagerSystem.Web.Models.GroupViewModels;
using EventManagerSystem.Web.Models.PostPublicationsViewModels;

namespace EventManagerSystem.Web.Controllers
{
    public class PostPublicationsController : Controller
    {

        // GET: Events
        public ActionResult Index()
        {
            PostPublicationsRepository repository = new PostPublicationsRepository();
            List<PostPublications> postPublications = repository.GetAll();
            postPublications.Reverse();

            PostPublicationsListViewModel model = new PostPublicationsListViewModel();
            model.PostPublications = postPublications;

            return View(model);
        }

        [AuthenticationFilter(RequireAdminRole = false)]
        public ActionResult Create()
        {
                    return View();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Create(CRUDPostPublicationsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            PostPublications postPublications = new PostPublications();
            // events.Id = model.Id;
            postPublications.Title = model.Title;
            postPublications.Content = model.Content;
            postPublications.ImgURL = model.ImgURL;
            postPublications.Teacher = model.Teacher;
            //   postPublications.Teacher = model.Teacher;


            var repository = new PostPublicationsRepository();
            repository.Insert(postPublications);

            return RedirectToAction("Index");
        }

        [AuthenticationFilter(RequireAdminRole = false)]
        [System.Web.Mvc.HttpGet]
        public ActionResult Edit(int? id)
        {

            PostPublicationsRepository repository = new PostPublicationsRepository();

            CRUDPostPublicationsViewModel model = new CRUDPostPublicationsViewModel();

            if (id.HasValue)
            {
                PostPublications postPublications = repository.GetById(id.Value);
                model.Id = postPublications.Id;
                model.Title = postPublications.Title;
                model.Content = postPublications.Content;
                model.ImgURL =  postPublications.ImgURL;
                model.Teacher = postPublications.Teacher;
                //  model.Teacher = postPublications.Teacher;

            }

            return View(model);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(CRUDPostPublicationsViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            PostPublicationsRepository repository = new PostPublicationsRepository();

            PostPublications postPublications = new PostPublications();
            postPublications.Id = model.Id;


            postPublications.Title = model.Title;
            postPublications.Content = model.Content;
            postPublications.ImgURL = model.ImgURL;
            postPublications.Teacher = model.Teacher;
            //  postPublications.Teacher = model.Teacher;

            repository.Save(postPublications);

            return RedirectToAction("Index");
        }

        [AuthenticationFilter(RequireAdminRole = false)]
        [System.Web.Mvc.HttpGet]
        public ActionResult Delete(int id)
        {

            PostPublicationsRepository repository = new PostPublicationsRepository();

            PostPublications postPublications = repository.GetById(id);

            CRUDPostPublicationsViewModel model = new CRUDPostPublicationsViewModel();
            model.Title = postPublications.Title;
            model.Content = postPublications.Content;
            model.ImgURL = postPublications.ImgURL;
            model.Teacher = postPublications.Teacher;
            //model.Teacher = postPublications.Teacher;


            return View(model);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Delete(CRUDPostPublicationsViewModel model)
        {

            PostPublicationsRepository repository = new PostPublicationsRepository();
            if (model.Id.ToString() != String.Empty)
            {
                repository.Delete(model.Id);
            }


            return RedirectToAction("Index");
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Show(int id)
        {

            PostPublicationsRepository repository = new PostPublicationsRepository();

            PostPublications postPublications = repository.GetById(id);

            CRUDPostPublicationsViewModel model = new CRUDPostPublicationsViewModel();
            model.Title = postPublications.Title;
            model.Content = postPublications.Content;
            model.ImgURL = postPublications.ImgURL;
            model.Teacher = postPublications.Teacher;
            // model.Teacher = postPublications.Teacher;


            return View(model);
        }

        public ActionResult GetAllPostPublications()
        {
            return View();
        }

        public JsonResult GetPostPublications()
        {
            PostPublicationsRepository repository = new PostPublicationsRepository();

            List<PostPublications> groups = repository.GetAll();

            return new JsonResult { Data = groups, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}