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
using CinemaTicketBooking.Models.SuperAdminViewModels;

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

        [HttpGet]
        public ActionResult Index(MoviesFilterViewModel model)
        {

            ViewData["CityId"] = new SelectList(_context.TblCities.Where(f => f.IsDeleted == false), "CityId", "CityName");
            ViewData["CountryId"] = new SelectList(_context.TblCountries.Where(f => f.IsDeleted == false), "CountryId", "CountryName");
            ViewData["LanguageId"] = new SelectList(_context.TblLanguage.Where(f => f.IsDeleted == false), "LanguageId", "LanguageName");
            ViewData["MovieGenreId"] = new SelectList(_context.TblMovieGenre.Where(f => f.IsDeleted == false), "MovieGenreId", "MovieGenreName");
            ViewData["CinemaId"] = new SelectList(_context.TblCinema.Where(f => f.IsDeleted == false), "CinemaId", "CinemaName");

            var listOfAllMovies = _movieService.GetFilteredMovies(model);

            foreach(var item in listOfAllMovies)
            {
                var newdescription = item.MovieDescription.Length <= 60 ? item.MovieDescription : item.MovieDescription.Substring(0, 60) + "...";
                item.MovieDescription = newdescription;
            }

            return View(listOfAllMovies);

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
