using EventManagerSystem.Data.Entity.Repositories;
using EventManagerSystem.Entity;
using EventManagerSystem.Web.Filters;
using EventManagerSystem.Web.Models.HomeworkViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventManagerSystem.Web.Controllers
{
    public class HomeworkController : Controller
    {

        static UserRepository userRepository = new UserRepository();
        User user = userRepository.GetById(LoginFilter.GetUserId());

        // GET: Events
        public ActionResult Index()
        {

            HomeworkRepository repository = new HomeworkRepository();
            List<Homework> postPublications = repository.GetAll();
            postPublications.Reverse();

            HomeworkListViewModel model = new HomeworkListViewModel();
            model.Homeworks = postPublications;

            return View(model);
        }

        [AuthenticationFilter(RequireAdminRole = false)]
        public ActionResult Create()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Create(CRUDHomeworkViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Homework homework = new Homework();
            // events.Id = model.Id;
            homework.Title = model.Title;
            homework.Content = model.Content;
            homework.StudentId = user.Id;
      


            var repository = new HomeworkRepository();
            repository.Insert(homework);

            return RedirectToAction("Index");
        }

        [AuthenticationFilter(RequireAdminRole = false)]
        [System.Web.Mvc.HttpGet]
        public ActionResult Edit(int? id)
        {

            HomeworkRepository repository = new HomeworkRepository();

            CRUDHomeworkViewModel model = new CRUDHomeworkViewModel();

            if (id.HasValue)
            {
                Homework homework = repository.GetById(id.Value);
                model.Id = homework.Id;
                model.Title = homework.Title;
                model.Content = homework.Content;
                homework.StudentId = user.Id;

            }

            return View(model);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(CRUDHomeworkViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            HomeworkRepository repository = new HomeworkRepository();

            Homework homework = new Homework();
            homework.Id = model.Id;


            homework.Title = model.Title;
            homework.Content = model.Content;
            homework.StudentId = user.Id;

            repository.Save(homework);

            return RedirectToAction("Index");
        }

        [AuthenticationFilter(RequireAdminRole = false)]
        [System.Web.Mvc.HttpGet]
        public ActionResult Delete(int id)
        {

            HomeworkRepository repository = new HomeworkRepository();

            Homework homework = repository.GetById(id);

            CRUDHomeworkViewModel model = new CRUDHomeworkViewModel();
            model.Title = homework.Title;
            model.Content = homework.Content;
            model.StudentId = homework.StudentId;


            return View(model);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Delete(CRUDHomeworkViewModel model)
        {

            HomeworkRepository repository = new HomeworkRepository();
            if (model.Id.ToString() != String.Empty)
            {
                repository.Delete(model.Id);
            }


            return RedirectToAction("Index");
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Show(int id)
        {

            HomeworkRepository repository = new HomeworkRepository();

            Homework homework = repository.GetById(id);

            CRUDHomeworkViewModel model = new CRUDHomeworkViewModel();
            model.Title = homework.Title;
            model.Content = homework.Content;


            return View(model);
        }

        public ActionResult GetAllHomeworks()
        {
            return View();
        }

        public JsonResult GetHomeworks()
        {
            HomeworkRepository repository = new HomeworkRepository();

            List<Homework> groups = repository.GetAll();

            return new JsonResult { Data = groups, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}