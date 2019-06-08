using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CinemaTicketBooking.Models;
using CinemaTicketBooking.Entities;
using CinemaTicketBooking.Extensions;
using CinemaTicketBooking.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CinemaTicketBooking.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly CinemaTicketBookingContext _context;

        public HomeController(CinemaTicketBookingContext context, IMovieService movieService)
        {
            _context = context;
            _movieService = movieService;
        }

        public IActionResult Index()
        {

            var listOfAllMovies = _movieService.GetAllMovies();
            ViewData["ListOfMovies"] = listOfAllMovies;


            ViewData["CityId"] = new SelectList(_context.TblCities, "CityId", "CityName");
            ViewData["CountryId"] = new SelectList(_context.TblCountries, "CountryId", "CountryName");
            ViewData["LanguageId"] = new SelectList(_context.TblLanguage, "LanguageId", "LanguageName");
            ViewData["MovieGenreId"] = new SelectList(_context.TblMovieGenre, "MovieGenreId", "MovieGenreName");
            ViewData["CinemaId"] = new SelectList(_context.TblCinema, "CinemaId", "CinemaName");

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
