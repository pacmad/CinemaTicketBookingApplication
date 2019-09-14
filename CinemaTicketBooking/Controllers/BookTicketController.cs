using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketBooking.Controllers
{
    public class BookTicketController : Controller
    {
        public IActionResult ChoseSeat()
        {
            return View();
        }
    }
}