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
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IActionResult> GetFilteredMovies(MoviesFilterViewModel model)
        {

            try
            {
                var result = await _context.TblMovie
                    .Include(t => t.Cinema)
                    .Include(t => t.ImageNavigation)
                    .Include(t => t.Language)
                    .Include(t => t.Cinema)
                    //.Include(t => t.Ad)
                    .Include(t => t.MovieGenre)
                    .Include(t => t.TblCustomerComments)
                    .Include(t => t.TblReservations)
                    .Include(t => t.TblShowTime)
                    .Include(t => t.TblTicket)
                    .Where(r => r.IsDeleted == false)
                    .Where(t => t.Cinema.IsDeleted == false)
                    .ToListAsync();

                var queryableList = result.AsQueryable();

                if (model != null)
                {
                    if (model.MovieGenreId.HasValue)
                    {
                        queryableList = queryableList.Where(x => x.MovieGenreId == model.MovieGenreId);
                    }
                    if (model.CountryId.HasValue)
                    {
                        queryableList = queryableList.Where(x => x.Cinema.Adress.CountryId == model.CountryId);
                    }
                    if (model.CityId.HasValue)
                    {
                        queryableList = queryableList.Where(x => x.Cinema.Adress.CityId == model.CityId);
                    }
                    if (model.CinemaId.HasValue)
                    {
                        queryableList = queryableList.Where(x => x.CinemaId == model.CinemaId);
                    }
                    if (model.LanguageId.HasValue)
                    {
                        queryableList = queryableList.Where(x => x.LanguageId == model.LanguageId);
                    }
               
                }

                var list = queryableList.Select(s => new {
                    s.MovieId,
                    s.CinemaId,
                    s.MovieGenreId,
                    s.IsBookable,
                    s.MovieName,
                    s.MovieDescription,
                    s.ReleaseDate,
                    s.MovieLength,
                    s.PriceForAdults,
                    s.PriceForChildrens,
                    s.Rating,
                    s.LanguageId
                }).ToList();

                return Ok(list);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
