using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using Microsoft.Owin.Security.Provider;
using TicketBus.Models;

namespace TicketBus.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AutoComplete(string term)
        {
            return Json(SearchAutoComplete(term), JsonRequestBehavior.AllowGet);
        }

        public List<string> SearchAutoComplete(string name)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {              
                return db.BusStops.Where(p => p.Name.StartsWith(name))
                    .Select(p => p.Name).ToList();
            }
        }

        [HttpPost]
        public async Task<ActionResult> SearchResult(string first, string second, string date)
        {
            DateTime dateTime = Convert.ToDateTime(date);
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                IEnumerable<Voyage> res =
                    await 
                        db.Voyages.Include(x => x.BusStops).Where(y => (y.BusStops.Select(z => z.Name).Contains(first) 
                        && y.BusStops.Select(z => z.Name).Contains(second)) 
                        && (y.DepartureDateTime.Year == dateTime.Year
                        && y.DepartureDateTime.Month == dateTime.Month
                        && y.DepartureDateTime.Day == dateTime.Day)).ToListAsync();
                if (res == null)
                    return HttpNotFound();
                else { return PartialView("SearchResult", res); }             
            }
        }

        [HttpGet]
        public ActionResult Ticket(int id)
        {
            ViewBag.VoyageId = id;
            return View();
        }

        [HttpPost]
        public string Ticket(Ticket ticket)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Tickets.Add(ticket);
            }
            return "Thanks, " + ticket.PassengersFullName + ", for buying.";
        }

        public ActionResult Register()
        {
            return RedirectToAction("Register", "Account");
        }

        public ActionResult LogIn()
        {
            return RedirectToAction("LogIn", "Account");
        }
    }
}