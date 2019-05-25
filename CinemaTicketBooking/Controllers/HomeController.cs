using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CinemaTicketBooking.Models;
using CinemaTicketBooking.Entities;
using CinemaTicketBooking.Extensions;

namespace CinemaTicketBooking.Controllers
{
    public class HomeController : Controller
    {
        private readonly CinemaTicketBookingContext _context;

        public HomeController(CinemaTicketBookingContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";

            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveMessage(TblFeedback model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var cinemaAdded = _context.TblFeedback.Add(model);

            if (_context.SaveChanges() == 0)
            {
                return View("Contact").WithDanger("Info!", "Your comment could not be saved!");
            }

            return View("Index").WithSuccess("Info!", "Your comment was saved successfully!");
        }

    }
}
