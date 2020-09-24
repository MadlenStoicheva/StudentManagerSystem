using EventManagerSystem.Common.Helper;
using EventManagerSystem.Data.Entity.Repositories;
using EventManagerSystem.Entity;
using EventManagerSystem.NotificationServices;
using EventManagerSystem.Web.Filters;
using EventManagerSystem.Web.Models.TeacherViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EventManagerSystem.Web.Controllers
{
    public class TeacherController : Controller
    {
        private SendConfirmEmail sendConfirmEmail = new SendConfirmEmail();

        [AuthenticationFilter(RequireAdminRole = true)]
        // GET: User
        public ActionResult Index()
        {
            TeacherRepository repository = new TeacherRepository();
            List<Teacher> teachers = repository.GetAll();

            TeacherListViewModel model = new TeacherListViewModel();
            model.Teachers = teachers;

            return View(model);
        }

        [AuthenticationFilter(RequireAdminRole = true)]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CRUDTeacherViewModel model)
        {
            string validationCode = HashUtils.CreateReferralCode();
            var repository = new TeacherRepository();
            SendConfirmEmail emailSender = new SendConfirmEmail();

            List<Teacher> teachers = repository.GetAll();

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (teachers.Where(u => u.Email == model.Email).Any())
            {
                ModelState.AddModelError("error_email", "This email is already taken!");
                return View();
                //return View("Error");
            }
            else if (teachers.Where(u => u.Username == model.Username).Any())
            {

                ModelState.AddModelError("error_msg", "This username is already taken!");
                return View();
                // return View("Error");
            }
            else
            {


                Teacher teacher = new Teacher();
                // user.Id = model.Id;
                teacher.ImgURL = model.ImgURL;
                teacher.Username = model.Username;
                teacher.Password = model.Password;
                teacher.Email = model.Email;
                teacher.FirstName = model.FirstName;
                teacher.LastName = model.LastName;
                teacher.IsAdmin = model.IsAdmin;
                teacher.IsEmailConfirmed = false;
                teacher.ValidationCode = validationCode;
                teacher.Subject = model.Subject;
                teacher.IsTeacher = model.IsTeacher;

                repository.Insert(teacher);

                sendConfirmEmail.SendConfirmationEmailAsync(teacher);
            }
            return RedirectToAction("Index");
        }

        [AuthenticationFilter(RequireAdminRole = true)]
        [HttpGet]
        public ActionResult Edit(int? id)
        {

            TeacherRepository repository = new TeacherRepository();

            CRUDTeacherViewModel model = new CRUDTeacherViewModel();

            if (id.HasValue)
            {
                Teacher teacher = repository.GetById(id.Value);
                model.Id = teacher.Id;
                model.ImgURL = teacher.ImgURL;
                model.Username = teacher.Username;
                model.Password = teacher.Password;
                model.Email = teacher.Email;
                model.FirstName = teacher.FirstName;
                model.LastName = teacher.LastName;
                model.IsAdmin = teacher.IsAdmin;
                model.Subject = teacher.Subject;
                model.IsTeacher = teacher.IsTeacher;
                // model.IsEmailConfirmed = user.IsEmailConfirmed;
                // model.ValidationCode = user.ValidationCode;

            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CRUDTeacherViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            TeacherRepository repository = new TeacherRepository();

            Teacher teacher = new Teacher();
            teacher.Id = model.Id;
            teacher.ImgURL = model.ImgURL;
            teacher.Username = model.Username;
            teacher.Password = model.Password;
            teacher.Email = model.Email;
            teacher.FirstName = model.FirstName;
            teacher.LastName = model.LastName;
            teacher.IsAdmin = model.IsAdmin;
            teacher.Subject = model.Subject;
            teacher.IsTeacher = model.IsTeacher;
            //user.IsEmailConfirmed = model.IsEmailConfirmed;
            //user.ValidationCode = model.ValidationCode;

            repository.Save(teacher);

            return RedirectToAction("Index");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(CRUDTeacherViewModel model)
        {
            string validationCode = HashUtils.CreateReferralCode();
            var repository = new TeacherRepository();
            List<Teacher> teachers = repository.GetAll();

            SendConfirmEmail emailSender = new SendConfirmEmail();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (teachers.Where(u => u.Email == model.Email).Any())
            {
                ModelState.AddModelError("error_email", "This email is already taken!");
                return View();
                //return View("Error");
            }
            else if (teachers.Where(u => u.Username == model.Username).Any())
            {

                ModelState.AddModelError("error_msg", "This username is already taken!");
                return View();
                // return View("Error");
            }
            else
            {

                Teacher teacher = new Teacher();
                teacher.ImgURL = model.ImgURL;
                teacher.Username = model.Username;
                teacher.Password = model.Password;
                teacher.Email = model.Email;
                teacher.FirstName = model.FirstName;
                teacher.LastName = model.LastName;
                teacher.IsAdmin = model.IsAdmin;
                teacher.IsEmailConfirmed = false;
                teacher.ValidationCode = validationCode;
                teacher.Subject = model.Subject;
                teacher.IsTeacher = model.IsTeacher;

                repository.Insert(teacher);

                sendConfirmEmail.SendConfirmationEmailAsync(teacher);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult EditProfile()
        {

            int id = LoginFilter.GetUserId();
            TeacherRepository repository = new TeacherRepository();
            CRUDTeacherViewModel model = new CRUDTeacherViewModel();
            Teacher teacher = repository.GetById(id);

            model.Id = LoginFilter.GetUserId();
            model.ImgURL = teacher.ImgURL;
            model.Username = teacher.Username;
            model.Password = teacher.Password;
            model.Email = teacher.Email;
            model.FirstName = teacher.FirstName;
            model.LastName = teacher.LastName;
         //   teacher.IsAdmin = model.IsAdmin;
            model.Subject = teacher.Subject;
            model.IsTeacher = teacher.IsTeacher;

            return View(model);
        }

        [HttpPost]
        public ActionResult EditProfile(CRUDTeacherViewModel model)
        {
            TeacherRepository repository = new TeacherRepository();

            Teacher teacher = repository.GetById(model.Id);

            teacher.Id = model.Id;
            teacher.ImgURL = model.ImgURL;
            teacher.Username = model.Username;
            teacher.Password = model.Password;
            teacher.Email = model.Email;
            teacher.FirstName = model.FirstName;
            teacher.LastName = model.LastName;
            teacher.IsAdmin = model.IsAdmin;
            teacher.Subject = model.Subject;
            teacher.IsTeacher = model.IsTeacher;

            repository.Save(teacher);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Error()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {

            TeacherRepository repository = new TeacherRepository();

            Teacher teacher = repository.GetById(id);

            CRUDTeacherViewModel model = new CRUDTeacherViewModel();
            model.ImgURL = teacher.ImgURL;
            model.Username = teacher.Username;
            model.Password = teacher.Password;
            model.Email = teacher.Email;
            model.FirstName = teacher.FirstName;
            model.LastName = teacher.LastName;
            model.IsAdmin = teacher.IsAdmin;
            model.IsEmailConfirmed = teacher.IsEmailConfirmed;
            model.ValidationCode = teacher.ValidationCode;
            model.Subject = teacher.Subject;
            model.IsTeacher = teacher.IsTeacher;

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(CRUDTeacherViewModel model)
        {
            int id = LoginFilter.GetUserId();

            TeacherRepository repository = new TeacherRepository();
            if (model.Id.ToString() != String.Empty)
            {
                repository.Delete(model.Id);
            }
            if (model.Id == id)
            {
                return RedirectToAction("Logout", "Home");
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ValidateEmail(string userId, string validationCode)
        {
            if (userId == null || validationCode == null)
            {
                return RedirectToAction("IndexPage", "Home");
            }

            TeacherRepository repository = new TeacherRepository();

            Teacher teacher = repository.GetById(Int32.Parse(userId));
            if (teacher == null || validationCode != teacher.ValidationCode)
            {
                return RedirectToAction("IndexPage", "Home");
            }

            teacher.Id = Int32.Parse(userId);
            teacher.ValidationCode = validationCode;
            teacher.IsEmailConfirmed = true;

            repository.Update(teacher);

            return View("ConfirmEmail");
        }
        public ActionResult ConfirmEmail()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ShowProfile()
        {

            int id = LoginFilter.GetUserId();
            TeacherRepository repository = new TeacherRepository();
            CRUDTeacherViewModel model = new CRUDTeacherViewModel();
            Teacher teacher = repository.GetById(id);

            model.Id = LoginFilter.GetUserId();
            model.ImgURL = teacher.ImgURL;
            model.Username = teacher.Username;
            model.Password = teacher.Password;
            model.Email = teacher.Email;
            model.FirstName = teacher.FirstName;
            model.LastName = teacher.LastName;
            teacher.IsAdmin = model.IsAdmin;
            model.Subject = teacher.Subject;
            model.IsTeacher = teacher.IsTeacher;

            return View(model);
        }
    }
}