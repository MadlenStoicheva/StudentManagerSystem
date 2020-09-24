using EventManagerSystem.Data.Entity.Repositories;
using EventManagerSystem.Entity;
using EventManagerSystem.Web.Filters;
using EventManagerSystem.Web.Models.GroupViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace EventManagerSystem.Web.Controllers
{
    public class GroupController : Controller
    {

        // GET: Events
        public ActionResult Index()
        {
            GroupRepository repository = new GroupRepository();
            List<Group> groups = repository.GetAll();
            groups.Reverse();

            GroupListViewModel model = new GroupListViewModel();
            model.Groups = groups;

            return View(model);
        }

        //
        private List<SelectListItem> PopuateStudentsList()
        {
            List<SelectListItem> result = new List<SelectListItem>();
            StudentRepository studentRepo = new StudentRepository();
            List<Student> students = studentRepo.GetAll();
            foreach (Student student in students)
            {
                SelectListItem item = new SelectListItem();
                item.Value = student.Id.ToString();
                item.Text = $"{student.FirstName} {student.LastName}";

                result.Add(item);
            }

            return result;
        }

        private List<SelectListItem> PopuateTeachersList()
        {
            List<SelectListItem> result = new List<SelectListItem>();
            TeacherRepository teacherRepo = new TeacherRepository();
            List<Teacher> teachers = teacherRepo.GetAll();
            foreach (Teacher teacher in teachers)
            {
                SelectListItem item = new SelectListItem();
                item.Value = teacher.Id.ToString();
                item.Text = $"{teacher.FirstName} {teacher.LastName}";

                result.Add(item);
            }

            return result;
        }
        //

        [AuthenticationFilter(RequireAdminRole = true)]
        public ActionResult Create()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Create(CRUDGroupViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Group groups = new Group();
           
            groups.Name = model.Name;
          //  model.Students = PopuateStudentsList();
          //  model.Teachers = PopuateTeachersList();
            groups.Student = model.Student;
            groups.Teacher = model.Teacher;


            var repository = new GroupRepository();
            repository.Insert(groups);

            return RedirectToAction("Index");
        }

        [AuthenticationFilter(RequireAdminRole = true)]
        [System.Web.Mvc.HttpGet]
        public ActionResult Edit(int? id)
        {

            GroupRepository repository = new GroupRepository();

            CRUDGroupViewModel model = new CRUDGroupViewModel();

            if (id.HasValue)
            {
                Group groups = repository.GetById(id.Value);
                model.Id = groups.Id;
                model.Name = groups.Name;
                model.Student = groups.Student;
                model.Teacher = groups.Teacher;

            }

            return View(model);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(CRUDGroupViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            GroupRepository repository = new GroupRepository();

            Group group = new Group();
            group.Id = model.Id;
            group.Student = model.Student;
            group.Teacher = model.Teacher;
            group.Name = model.Name;

            repository.Save(group);

            return RedirectToAction("Index");
        }

        [AuthenticationFilter(RequireAdminRole = true)]
        [System.Web.Mvc.HttpGet]
        public ActionResult Delete(int id)
        {

            GroupRepository repository = new GroupRepository();

            Group group = repository.GetById(id);

            CRUDGroupViewModel model = new CRUDGroupViewModel();
            model.Name = group.Name;
            model.Student = group.Student;
            model.Teacher = group.Teacher;
            

            return View(model);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Delete(CRUDGroupViewModel model)
        {

            GroupRepository repository = new GroupRepository();
            if (model.Id.ToString() != String.Empty)
            {
                repository.Delete(model.Id);
            }


            return RedirectToAction("Index");
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Show(int id)
        {

            GroupRepository repository = new GroupRepository();

            Group group = repository.GetById(id);

            CRUDGroupViewModel model = new CRUDGroupViewModel();
            model.Name = group.Name;
            model.Student = group.Student;
            model.Teacher = group.Teacher;
          

            return View(model);
        }

        public ActionResult GetAllGroups()
        {
            return View();
        }

        public JsonResult GetGroups()
        {
            GroupRepository repository = new GroupRepository();

            List<Group> groups = repository.GetAll();

            return new JsonResult { Data = groups, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }

}
