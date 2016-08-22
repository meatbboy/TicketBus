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
        public ActionResult Ticket(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                bool flag = true;
                string first = null, second = null;
                ViewBag.Success = "Thanks, " + ticket.PassengersFullName + ", for buying!";
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    var res = db.Tickets.OrderBy(t => t.VoyageId).Select(t => new { first = t.VoyageId, second = t.PassengerSeatNumber });
                    foreach (var re in res)
                    {
                        if (re.first == ticket.VoyageId && re.second == ticket.PassengerSeatNumber)
                        {
                            ViewBag.Success = "This seat is taken!";
                            flag = false;
                        }
                    }
                    var temp = db.Tickets.OrderBy(t => t.VoyageId).Select(t => new { first = t.VoyageId, second = t.PassengersDocNumber });
                    foreach (var tem in temp)
                    {
                        if (tem.first == ticket.VoyageId && tem.second == ticket.PassengersDocNumber)
                        {
                            ViewBag.Success = "Passenger already registered!";
                            flag = false;
                        }   
                    }
                    if (flag)
                    {
                        db.Tickets.Add(ticket);
                        db.SaveChanges();
                    }
                }
                return View();
            }
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            ViewBag.Success = "Fill data!";
            return View();
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