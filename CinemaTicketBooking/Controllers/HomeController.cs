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

            foreach (var item in listOfAllMovies)
            {
                var newdescription = item.MovieDescription.Length <= 60 ? item.MovieDescription : item.MovieDescription.Substring(0, 60) + "...";
                item.MovieDescription = newdescription;
            }

            return View(listOfAllMovies);
        }

        [HttpGet]
        public async Task<JsonResult> GetFilteredMovies(MoviesFilterViewModel model)
        {
            try
            {
                var movies = await _context.TblMovie
                .Include(t => t.ImageNavigation)
                .Include(t => t.Language)
                .Include(t => t.Cinema)
                .ThenInclude(t => t.Adress)
                .Include(t => t.MovieGenre)
                .Include(t => t.TblCustomerComments)
                .Include(t => t.TblReservations)
                .Include(t => t.TblShowTime)
                .Include(t => t.TblTicket)
                .Where(r => r.IsDeleted == false)
                .Where(t => t.Cinema.IsDeleted == false)
                .ToListAsync();


                var targetList = movies
              .Select(x => new MovieViewModel()
              {
                  CinemaId = x.CinemaId,
                  LanguageId = x.LanguageId,
                  MovieId = x.MovieId,
                  MovieGenreId = x.MovieGenreId ?? 1,
                  IsBookable = x.IsBookable,
                  MovieName = x.MovieName ?? "",
                  MovieDescription = x.MovieDescription ?? "",
                  ReleaseDate = x.ReleaseDate ?? "",
                  MovieLength = x.MovieLength ?? "",
                  PriceForAdults = x.PriceForAdults ?? "",
                  PriceForChildrens = x.PriceForChildrens ?? "",
                  ShowTime = x.TblShowTime.FirstOrDefault().Time.Split(' ')[0],
                  Rating = x.Rating ?? "",
                  ImagePath = x.ImageNavigation.ImagePath ?? "",
                  CinemaName = x.Cinema.CinemaName,
                  CityId = x.Cinema.Adress.CityId,
                  CountryId = x.Cinema.Adress.CountryId
              })
              .ToList();

                if (model != null)
                {
                    if (model.MovieGenreId.HasValue)
                    {
                        targetList = targetList.Where(x => x.MovieGenreId == model.MovieGenreId).ToList();
                    }
                    if (model.CountryId.HasValue)
                    {
                        targetList = targetList.Where(x => x.CountryId == model.CountryId).ToList();
                    }
                    if (model.CityId.HasValue)
                    {
                        targetList = targetList.Where(x => x.CityId == model.CityId).ToList();
                    }
                    if (model.CinemaId.HasValue)
                    {
                        targetList = targetList.Where(x => x.CinemaId == model.CinemaId).ToList();
                    }
                    if (model.LanguageId.HasValue)
                    {
                        targetList = targetList.Where(x => x.LanguageId == model.LanguageId).ToList();
                    }
                }


                return Json(targetList);
            }
            catch (Exception ex)
            {
                return Json(ex);
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
