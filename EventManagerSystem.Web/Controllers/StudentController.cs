using EventManagerSystem.Common.Helper;
using EventManagerSystem.Data.Entity.Repositories;
using EventManagerSystem.Entity;
using EventManagerSystem.NotificationServices;
using EventManagerSystem.Web.Filters;
using EventManagerSystem.Web.Models.StudentViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EventManagerSystem.Web.Controllers
{
    public class StudentController : Controller
    {
        private SendConfirmEmail sendConfirmEmail = new SendConfirmEmail();

        [AuthenticationFilter(RequireAdminRole = true)]
        // GET: User
        public ActionResult Index()
        {
            StudentRepository repository = new StudentRepository();
            List<Student> students = repository.GetAll();

            StudentListViewModel model = new StudentListViewModel();
            model.Students = students;

            return View(model);
        }

        [AuthenticationFilter(RequireAdminRole = true)]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CRUDStudentViewModel model)
        {
            string validationCode = HashUtils.CreateReferralCode();
            var repository = new StudentRepository();
            SendConfirmEmail emailSender = new SendConfirmEmail();

            List<Student> students = repository.GetAll();

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (students.Where(u => u.Email == model.Email).Any())
            {
                ModelState.AddModelError("error_email", "This email is already taken!");
                return View();
                //return View("Error");
            }
            else if (students.Where(u => u.Username == model.Username).Any())
            {

                ModelState.AddModelError("error_msg", "This username is already taken!");
                return View();
                // return View("Error");
            }
            else
            {


                Student student = new Student();
                // user.Id = model.Id;
                student.ImgURL = model.ImgURL;
                student.Username = model.Username;
                student.Password = model.Password;
                student.Email = model.Email;
                student.FirstName = model.FirstName;
                student.LastName = model.LastName;
                student.IsEmailConfirmed = false;
                student.ValidationCode = validationCode;
                student.FacultyNumber = model.FacultyNumber;
                student.Specialty = model.Specialty;
                student.GroupId = model.GroupId;
                student.IsStudent = model.IsStudent;
                student.IsTeacher = model.IsTeacher;

                repository.Insert(student);

                sendConfirmEmail.SendConfirmationEmailAsync(student);
            }
            return RedirectToAction("Index");
        }

        [AuthenticationFilter(RequireAdminRole = true)]
        [HttpGet]
        public ActionResult Edit(int? id)
        {

            StudentRepository repository = new StudentRepository();

            CRUDStudentViewModel model = new CRUDStudentViewModel();

            if (id.HasValue)
            {
                Student student = repository.GetById(id.Value);
                model.Id = student.Id;
                model.ImgURL = student.ImgURL;
                model.Username = student.Username;
                model.Password = student.Password;
                model.Email = student.Email;
                model.FirstName = student.FirstName;
                model.LastName = student.LastName;
                model.IsAdmin = student.IsAdmin;
                model.FacultyNumber = student.FacultyNumber; 
                model.Specialty = student.Specialty;
                model.GroupId = student.GroupId;
                model.IsTeacher = student.IsTeacher;
                model.IsStudent = student.IsStudent;
                // model.IsEmailConfirmed = user.IsEmailConfirmed;
                // model.ValidationCode = user.ValidationCode;

            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CRUDStudentViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            StudentRepository repository = new StudentRepository();

            Student student = new Student();
            student.Id = model.Id;
            student.ImgURL = model.ImgURL;
            student.Username = model.Username;
            student.Password = model.Password;
            student.Email = model.Email;
            student.FirstName = model.FirstName;
            student.LastName = model.LastName;
            student.IsAdmin = model.IsAdmin;
            student.FacultyNumber = model.FacultyNumber;
            student.Specialty = model.Specialty;
            student.GroupId = model.GroupId;
            student.IsStudent = model.IsStudent;
            student.IsTeacher = model.IsTeacher;
            //user.IsEmailConfirmed = model.IsEmailConfirmed;
            //user.ValidationCode = model.ValidationCode;

            repository.Save(student);

            return RedirectToAction("Index");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(CRUDStudentViewModel model)
        {
            string validationCode = HashUtils.CreateReferralCode();
            var repository = new StudentRepository();
            List<Student> students = repository.GetAll();

            SendConfirmEmail emailSender = new SendConfirmEmail();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (students.Where(u => u.Email == model.Email).Any())
            {
                ModelState.AddModelError("error_email", "This email is already taken!");
                return View();
                //return View("Error");
            }
            else if (students.Where(u => u.Username == model.Username).Any())
            {

                ModelState.AddModelError("error_msg", "This username is already taken!");
                return View();
                // return View("Error");
            }
            else
            {

                Student student = new Student();
                student.ImgURL = model.ImgURL;
                student.Username = model.Username;
                student.Password = model.Password;
                student.Email = model.Email;
                student.FirstName = model.FirstName;
                student.LastName = model.LastName;
                student.IsAdmin = model.IsAdmin;
                student.IsEmailConfirmed = false;
                student.ValidationCode = validationCode;
                student.FacultyNumber = model.FacultyNumber;
                student.Specialty = model.Specialty;
                student.GroupId = model.GroupId;
                student.IsTeacher = model.IsTeacher;
                student.IsStudent = model.IsStudent;

                repository.Insert(student);

                sendConfirmEmail.SendConfirmationEmailAsync(student);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult EditProfile()
        {

            int id = LoginFilter.GetUserId();
            StudentRepository repository = new StudentRepository();
            CRUDStudentViewModel model = new CRUDStudentViewModel();
            Student student = repository.GetById(id);

            model.Id = LoginFilter.GetUserId();
            model.ImgURL = student.ImgURL;
            model.Username = student.Username;
            model.Password = student.Password;
            model.Email = student.Email;
            model.FirstName = student.FirstName;
            model.LastName = student.LastName;
            student.IsAdmin = model.IsAdmin;
            model.FacultyNumber = student.FacultyNumber;
            model.Specialty = student.Specialty;
            model.GroupId = student.GroupId;
            model.IsStudent = student.IsStudent;
            model.IsTeacher = student.IsTeacher;

            return View(model);
        }

        [HttpPost]
        public ActionResult EditProfile(CRUDStudentViewModel model)
        {
            StudentRepository repository = new StudentRepository();

            Student student = repository.GetById(model.Id);

            student.Id = model.Id;
            student.ImgURL = model.ImgURL;
            student.Username = model.Username;
            student.Password = model.Password;
            student.Email = model.Email;
            student.FirstName = model.FirstName;
            student.LastName = model.LastName;
            student.IsAdmin = model.IsAdmin;
            student.FacultyNumber = model.FacultyNumber;
            student.Specialty = model.Specialty;
            student.GroupId = model.GroupId;
            student.IsTeacher = model.IsTeacher;
            student.IsStudent = model.IsStudent;


            repository.Save(student);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Error()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {

            StudentRepository repository = new StudentRepository();

            Student student = repository.GetById(id);

            CRUDStudentViewModel model = new CRUDStudentViewModel();
            model.ImgURL = student.ImgURL;
            model.Username = student.Username;
            model.Password = student.Password;
            model.Email = student.Email;
            model.FirstName = student.FirstName;
            model.LastName = student.LastName;
            model.IsAdmin = student.IsAdmin;
            model.IsEmailConfirmed = student.IsEmailConfirmed;
            model.ValidationCode = student.ValidationCode;
            model.FacultyNumber = student.FacultyNumber;
            model.Specialty = student.Specialty;
            model.GroupId = student.GroupId;
            model.IsStudent = student.IsStudent;
            model.IsTeacher = student.IsTeacher;

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(CRUDStudentViewModel model)
        {
            int id = LoginFilter.GetUserId();

            StudentRepository repository = new StudentRepository();
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

            StudentRepository repository = new StudentRepository();

            Student student = repository.GetById(Int32.Parse(userId));
            if (student == null || validationCode != student.ValidationCode)
            {
                return RedirectToAction("IndexPage", "Home");
            }

            student.Id = Int32.Parse(userId);
            student.ValidationCode = validationCode;
            student.IsEmailConfirmed = true;

            repository.Update(student);

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
            StudentRepository repository = new StudentRepository();
            CRUDStudentViewModel model = new CRUDStudentViewModel();
            Student student = repository.GetById(id);

            model.Id = LoginFilter.GetUserId();
            model.ImgURL = student.ImgURL;
            model.Username = student.Username;
            model.Password = student.Password;
            model.Email = student.Email;
            model.FirstName = student.FirstName;
            model.LastName = student.LastName;
            student.IsAdmin = model.IsAdmin;
            model.FacultyNumber = student.FacultyNumber;
            model.Specialty = student.Specialty;
            model.GroupId = student.GroupId;
            model.IsTeacher = student.IsTeacher;
            model.IsStudent = student.IsStudent;

            return View(model);
        }
    }
}