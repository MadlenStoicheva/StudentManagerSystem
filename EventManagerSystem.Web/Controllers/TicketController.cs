using EventManagerSystem.Data.Entity.Repositories;
using EventManagerSystem.Entity;
using EventManagerSystem.Web.Filters;
using EventManagerSystem.Web.Models.EventsViewModels;
using EventManagerSystem.Web.Models.TicketsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventManagerSystem.Web.Controllers
{
    public class TicketController : Controller
    {
        [AuthenticationFilter(RequireAdminRole = true)]

        // GET: Ticket
        public ActionResult Index()
        {
            TicketRepository repository = new TicketRepository();

            List<Ticket> ticket = repository.GetAll();

            TicketsListViewModel model = new TicketsListViewModel();
            model.Tickets = ticket;


            return View(model);
        }

        public ActionResult Create(EventsDeleteViewModel model, int id)
        {
           
            EventRepository repository = new EventRepository();

            Event events = repository.GetById(id);

            model.ImgURL = events.ImgURL;
            model.Title = events.Title;
            model.EventDate = events.EventDate;
            model.EventPlace = events.EventPlace;
            model.Organizer = events.Organizer;
            model.Description = events.Description;

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(int id)
        {
            int userId = LoginFilter.GetUserId();

            Ticket ticket = new Ticket();
            ticket.EventId = id;
            ticket.UserId = userId;
           
           
            
            var repository = new TicketRepository();
            repository.Insert(ticket);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {

            TicketRepository ticketRepository = new TicketRepository();

            Ticket tickets = ticketRepository.GetById(id);

            EventRepository repository = new EventRepository();

            Event events = repository.GetById(tickets.EventId);

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
        public ActionResult Delete(DeleteTicketViewModel model)
        {

            TicketRepository repository = new TicketRepository();
            if (model.Id.ToString() != String.Empty)
            {
                repository.Delete(model.Id);
            }


            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ShowUserTickets()
        {
            int userId = LoginFilter.GetUserId();
            TicketRepository repository = new TicketRepository();
            List<Ticket> tickets = repository.GetAll();

            List<Ticket> userTickets = new List<Ticket>();

            foreach (var tick in tickets)
            {
                if (tick.UserId == userId)
                {
                    userTickets.Add(tick);
                }
                else
                {
                    ViewBag.Message = "This user has no tickets yet!";
                   // return RedirectToAction("Index", "Home");
                }
            }
          
           TicketsListViewModel model = new TicketsListViewModel();
            model.Tickets = userTickets;
           

            return View(model);

        }


    }
}