using EventManagerSystem.Data.Entity.Repositories;
using EventManagerSystem.Entity;
using EventManagerSystem.NotificationServices;
using EventManagerSystem.Web.Models.EmailSendingViewModel;
using EventManagerSystem.Web.Models.EventsViewModels;
using EventManagerSystem.Web.Models.LoginViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace EventManagerSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            EventRepository repository = new EventRepository();
            List<Event> events = repository.GetAll();
            events.Reverse();

            EventsListViewModel model = new EventsListViewModel();
            if (events.Count < 3)
            {
                model.Events = events;
            }
            else
            {
                model.Events = events.GetRange(0, 3);

            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(EmailSendingViewModel emailSendingViewModel)
        {
            EmailSender emailSender = new EmailSender();

            if (emailSendingViewModel.Comment == null || emailSendingViewModel.Name == null || emailSendingViewModel.Email == null)
            {
                ModelState.AddModelError("error_contact", "  You have to enter all the needed information for sending an email!");
                return View();
            }
            else
            {
                emailSender.SendEmail(emailSendingViewModel.Email, emailSendingViewModel.Name, emailSendingViewModel.Comment);
                TempData["Email"] = "You have send the email successfully!";
                return View("Contact");
            }

        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult NotConfirmedEmail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            UserRepository repo = new UserRepository();
            List<User> items = repo.GetAll(i => i.Username == model.Username && i.Password == model.Password);
            Session["LoggedUser"] = items.Count > 0 ? items[0] : null;

            Session["LogedUserUsername"] = model.Username;

            if (items.Count <= 0)
                this.ModelState.AddModelError("FailedLogin", "Login failed!");

            if (!ModelState.IsValid)
                return View(model);


            User user = EventManagerSystem.Web.Filters.LoginFilter.GetUserConfirm();
            var repository = new UserRepository();
            User users = repository.GetById(user.Id);

            if (users.IsEmailConfirmed == false)
            {
                ViewData["WrongLogin"] = "Email is not confirmed!";
                Logout();
                return View("NotConfirmedEmail");
            }
            else
            {

                return RedirectToAction("Index", "Home");
            }

           
        }

        public ActionResult Logout()
        {
            System.Web.HttpContext.Current.Session["LoggedUser"] = null;
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(EmailSendingViewModel emailSendingViewModel)
        {
            EmailSender emailSender = new EmailSender();

            if (emailSendingViewModel.Comment == null || emailSendingViewModel.Name == null || emailSendingViewModel.Email == null)
            {
                ModelState.AddModelError("error_contact", "  You have to enter all the needed information for sending an email!");
                return View();
            }
            else
            {
                emailSender.SendEmail(emailSendingViewModel.Email, emailSendingViewModel.Name, emailSendingViewModel.Comment);
                TempData["Email"] = "You have send the email successfully!";
                return View("Contact");
            }

        }


        [HttpGet]
       public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult FirstPublication()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SecondPublication()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ThirdPublication()
        {
            return View();
        }

    }
}