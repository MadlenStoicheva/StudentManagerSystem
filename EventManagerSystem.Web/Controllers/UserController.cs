using EventManagerSystem.Common.Helper;
using EventManagerSystem.Data.Entity.Repositories;
using EventManagerSystem.Entity;
using EventManagerSystem.NotificationServices;
using EventManagerSystem.Web.Filters;
using EventManagerSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EventManagerSystem.Web.Controllers
{
    public class UserController : Controller
    {
        private SendConfirmEmail sendConfirmEmail = new SendConfirmEmail();

        [AuthenticationFilter(RequireAdminRole = true)]
        // GET: User
        public ActionResult Index()
        {
            UserRepository repository = new UserRepository();
            List<User> users = repository.GetAll();

            UserListViewModel model = new UserListViewModel();
            model.Users = users;

            return View(model);
        }

        [AuthenticationFilter(RequireAdminRole = true)]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserCreateViewModel model)
        {
            string validationCode = HashUtils.CreateReferralCode();
            var repository = new UserRepository();
            SendConfirmEmail emailSender = new SendConfirmEmail();

            List<User> users = repository.GetAll();

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (users.Where(u => u.Email == model.Email).Any())
            {
                ModelState.AddModelError("error_email", "This email is already taken!");
                return View();
                //return View("Error");
            }
            else if (users.Where(u => u.Username == model.Username).Any())
            {

                ModelState.AddModelError("error_msg", "This username is already taken!");
                return View();
                // return View("Error");
            }
            else
            {


                User user = new User();
                // user.Id = model.Id;
                user.ImgURL = model.ImgURL;
                user.Username = model.Username;
                user.Password = model.Password;
                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.IsAdmin = model.IsAdmin;
                user.IsEmailConfirmed = false;
                user.ValidationCode = validationCode;
                user.IsStudent = model.IsStudent;
                user.IsTeacher = model.IsTeacher;

                repository.Insert(user);

                sendConfirmEmail.SendConfirmationEmailAsync(user);
            }
            return RedirectToAction("Index");
        }

        [AuthenticationFilter(RequireAdminRole = true)]
        [HttpGet]
        public ActionResult Edit(int? id)
        {

            UserRepository repository = new UserRepository();

            UserEditViewModel model = new UserEditViewModel();

            if (id.HasValue)
            {
                User user = repository.GetById(id.Value);
                model.Id = user.Id;
                model.ImgURL = user.ImgURL;
                model.Username = user.Username;
                model.Password = user.Password;
                model.Email = user.Email;
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.IsAdmin = user.IsAdmin;
                model.IsEmailConfirmed = user.IsEmailConfirmed;
                model.IsStudent = user.IsStudent;
                 model.IsTeacher = user.IsTeacher;
                // model.ValidationCode = user.ValidationCode;

            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(UserEditViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            UserRepository repository = new UserRepository();

            User user = new User();
            user.Id = model.Id;
            user.ImgURL = model.ImgURL;
            user.Username = model.Username;
            user.Password = model.Password;
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.IsAdmin = model.IsAdmin;
            user.IsEmailConfirmed = model.IsEmailConfirmed;
            user.IsStudent = model.IsStudent;
            user.IsTeacher = model.IsTeacher;
            // user.ValidationCode = model.ValidationCode;

            repository.Save(user);

            return RedirectToAction("Index");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(UserCreateViewModel model)
        {
            string validationCode = HashUtils.CreateReferralCode();
            var repository = new UserRepository();
            List<User> users = repository.GetAll();

            SendConfirmEmail emailSender = new SendConfirmEmail();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (users.Where(u => u.Email == model.Email).Any())
            {
                ModelState.AddModelError("error_email", "This email is already taken!");
                return View();
                //return View("Error");
            }
            else if (users.Where(u => u.Username == model.Username).Any())
            {

                ModelState.AddModelError("error_msg", "This username is already taken!");
                return View();
                // return View("Error");
            }
            else
            {

                User user = new User();
                user.ImgURL = model.ImgURL;
                user.Username = model.Username;
                user.Password = model.Password;
                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.IsAdmin = model.IsAdmin;
                user.IsEmailConfirmed = false;
                user.ValidationCode = validationCode;
                user.IsStudent = model.IsStudent;
                user.IsTeacher = model.IsTeacher;

                repository.Insert(user);

                sendConfirmEmail.SendConfirmationEmailAsync(user);
            }
           
            return RedirectToAction("Index", "Home");
        }

        public ActionResult EditProfile()
        {
            
            int id = LoginFilter.GetUserId();
            UserRepository repository = new UserRepository();
            UserEditViewModel model = new UserEditViewModel();
            User user = repository.GetById(id);

            model.Id = LoginFilter.GetUserId();
            model.ImgURL = user.ImgURL;
            model.Username = user.Username;
            model.Password = user.Password;
            model.Email = user.Email;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            user.IsAdmin = model.IsAdmin;
            user.IsStudent = model.IsStudent;
            user.IsTeacher = model.IsTeacher;
           
            return View(model);
        }

        [HttpPost]
        public ActionResult EditProfile(UserEditViewModel model)
        {
            UserRepository repository = new UserRepository();

            User user = repository.GetById(model.Id);
            
            user.Id = model.Id;
            user.ImgURL = model.ImgURL;
            user.Username = model.Username;
            user.Password = model.Password;
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.IsAdmin = model.IsAdmin;
            user.IsStudent = model.IsStudent;
            user.IsTeacher = model.IsTeacher;

            repository.Save(user);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Error()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {

            UserRepository repository = new UserRepository();

            User user = repository.GetById(id);

            UserDeleteViewModel model = new UserDeleteViewModel();
            model.ImgURL = user.ImgURL;
            model.Username = user.Username;
            model.Password = user.Password;
            model.Email = user.Email;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.IsAdmin = user.IsAdmin;
            model.IsEmailConfirmed = user.IsEmailConfirmed;
            model.ValidationCode = user.ValidationCode;
            user.IsStudent = model.IsStudent;
            user.IsTeacher = model.IsTeacher;

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(UserDeleteViewModel model)
        {
            int id = LoginFilter.GetUserId();

            UserRepository repository = new UserRepository();
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

            UserRepository repository = new UserRepository();

            User user = repository.GetById(Int32.Parse(userId));
            if (user == null || validationCode != user.ValidationCode)
            {
                return RedirectToAction("IndexPage", "Home");
            }

            user.Id = Int32.Parse(userId);
            user.ValidationCode = validationCode;
            user.IsEmailConfirmed = true;

            repository.Update(user);

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
            UserRepository repository = new UserRepository();
            UserEditViewModel model = new UserEditViewModel();
            User user = repository.GetById(id);

            model.Id = LoginFilter.GetUserId();
            model.ImgURL = user.ImgURL;
            model.Username = user.Username;
            model.Password = user.Password;
            model.Email = user.Email;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            user.IsAdmin = model.IsAdmin;
            user.IsStudent = model.IsStudent;
            user.IsTeacher = model.IsTeacher;

            return View(model);
        }

    }
}