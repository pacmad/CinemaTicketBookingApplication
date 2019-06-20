using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaTicketBooking.Entities;
using CinemaTicketBooking.Services;
using CinemaTicketBooking.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using CinemaTicketBooking.Models.SuperAdminViewModels;
using CinemaTicketBooking.Extensions;
using Microsoft.AspNetCore.Http;

namespace CinemaTicketBooking.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly CinemaTicketBookingContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;
        private readonly IImageHandler _imageHandler;

        public MoviesController(CinemaTicketBookingContext context,
            UserManager<ApplicationUser> userManager,
            ILogger<AccountController> logger,
            IMovieService movieService,
            IImageHandler imageHandler)
        {
            _userManager = userManager;
            _context = context;
            _logger = logger;
            _movieService = movieService;
            _imageHandler = imageHandler;
        }

        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            return await _imageHandler.UploadImage(file);
        }

        public IActionResult Index()
        {
            var listOfAllMovies = _movieService.GetAllMovies();
            foreach (var item in listOfAllMovies)
            {
                var newdescription = item.MovieDescription.Length <= 60 ? item.MovieDescription : item.MovieDescription.Substring(0, 60) + "...";
                item.MovieDescription = newdescription;
            }
            return View(listOfAllMovies.ToList());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myMovie = await _movieService.GetMovieById(id ?? 1);

            if (myMovie == null)
            {
                return NotFound();
            }

            return View(myMovie);
        }

        public IActionResult Create()
        {
            var showTimes = _context.TblShowTime.ToList();

            ViewData["CinemaId"] = new SelectList(_context.TblCinema.Where(r=>r.IsDeleted==false), "CinemaId", "CinemaName");
            ViewData["LanguageId"] = new SelectList(_context.TblLanguage, "LanguageId", "LanguageName");
            ViewData["MovieGenreId"] = new SelectList(_context.TblMovieGenre, "MovieGenreId", "GenreDescription");
            ViewData["ShowTimes"] = showTimes;


            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieViewModel model)
        {
            var user = await GetCurrentUserAsync();
            var userId = user?.Id;
            string mail = user?.Email;

            //var cinema = _context.TblCinema.Where(r => r.AdminUserId == userId).FirstOrDefault();

            model.CreatedByUserId = userId;
            model.LastModifiedByUserId = userId;
            //model.CinemaId = cinema.CinemaId;

            var result = await UploadImage(model.Image);
            var test = result as ObjectResult;

            model.ImagePath = test.Value.ToString();


            var cinemaAdded = _movieService.AddMovie(model);

            if (cinemaAdded)
            {
                var listOfAllCinemas = _movieService.GetAllMovies();
                foreach (var item in listOfAllCinemas)
                {
                    var newdescription = item.MovieDescription.Length <= 60 ? item.MovieDescription : item.MovieDescription.Substring(0, 60) + "...";
                    item.MovieDescription = newdescription;
                }
                return View("Index", listOfAllCinemas.ToList()).WithSuccess("Info!", "Movie was added successfully!");
            }

            return BadRequest();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieService.GetMovieById(id ?? 1);

            if (movie == null)
            {
                return NotFound();
            }

            ViewData["CinemaId"] = new SelectList(_context.TblCinema, "CinemaId", "CinemaName", movie.CinemaId);
            ViewData["Image"] = new SelectList(_context.Images, "ImageId", "ImagePath", movie.Image);
            ViewData["LanguageId"] = new SelectList(_context.TblLanguage, "LanguageId", "LanguageName", movie.LanguageId);
            return View(movie);
        }

        public async Task<IActionResult> MovieProfile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myMovie = await _movieService.GetMovieById(id ?? 1);
            var showTime = _context.TblShowTime.Where(r => r.MovieId == myMovie.MovieId).FirstOrDefault();
            ViewData["ShowTime"] = showTime.Time;

            if (myMovie == null)
            {
                return NotFound();
            }

            return View(myMovie);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MovieViewModel model)
        {
            if (id != model.MovieId)
            {
                return NotFound();
            }
            var user = await GetCurrentUserAsync();
            var userId = user?.Id;
            string mail = user?.Email;

            model.LastModifiedByUserId = userId;

            var movieEdited = _movieService.EditMovie(model);

            if (movieEdited)
            {
                var listOfAllCinemas = _movieService.GetAllMovies();
                foreach (var item in listOfAllCinemas)
                {
                    var newdescription = item.MovieDescription.Length <= 60 ? item.MovieDescription : item.MovieDescription.Substring(0, 60) + "...";
                    item.MovieDescription = newdescription;
                }
                return View("Index", listOfAllCinemas.ToList()).WithSuccess("Info!", "Movie was edited successfully!");
            }
            else
            {
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mymovie = await _movieService.GetMovieById(id ?? 1);

            if (mymovie == null)
            {
                return NotFound();
            }

            return View(mymovie);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieDeleted = await _movieService.DeleteMovie(id);

            if (movieDeleted)
            {
                var listOfAllCinemas = _movieService.GetAllMovies();
                foreach (var item in listOfAllCinemas)
                {
                    var newdescription = item.MovieDescription.Length <= 60 ? item.MovieDescription : item.MovieDescription.Substring(0, 60) + "...";
                    item.MovieDescription = newdescription;
                }
                return View("Index", listOfAllCinemas.ToList()).WithSuccess("Info!", "Movie was deleted successfully!");
            }
            else
            {
                return BadRequest();
            }
        }

        private bool TblMovieExists(int id)
        {
            return _context.TblMovie.Any(e => e.MovieId == id);
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
