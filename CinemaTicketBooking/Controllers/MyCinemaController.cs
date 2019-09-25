using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemaTicketBooking.Entities;
using CinemaTicketBooking.Extensions;
using CinemaTicketBooking.Models;
using CinemaTicketBooking.Models.SuperAdminViewModels;
using CinemaTicketBooking.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CinemaTicketBooking.Controllers
{
    public class MyCinemaController : Controller
    {
        private readonly ICinemaService _cinemaService;
        private readonly IMovieService _movieService;
        private readonly CinemaTicketBookingContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;
        private readonly IImageHandler _imageHandler;

        public MyCinemaController(CinemaTicketBookingContext context,
            UserManager<ApplicationUser> userManager,
            ILogger<AccountController> logger,
            ICinemaService cinemaService,
            IImageHandler imageHandler,
            IMovieService movieService)
        {
            _userManager = userManager;
            _context = context;
            _logger = logger;
            _cinemaService = cinemaService;
            _movieService = movieService;
            _imageHandler = imageHandler;
        }

        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var userId = user?.Id;

            var tblCinema = await _cinemaService.GetCinemaByAdminId(userId);

            if (tblCinema == null)
            {
                return NotFound();
            }

            var listOfAllMovies = _movieService.GetMoviesByCinemaId(tblCinema.CinemaId);

            foreach (var item in listOfAllMovies)
            {
                var newdescription = item.MovieDescription.Length <= 60 ? item.MovieDescription : item.MovieDescription.Substring(0, 60) + "...";
                item.MovieDescription = newdescription;
            }

            ViewData["ListOfMovies"] = listOfAllMovies;

            return View(tblCinema);
        }

        public async Task<IActionResult> Dashboard()
        {
            try
            {
                var user = await GetCurrentUserAsync();
                var userId = user?.Id;

                var tblCinema = await _cinemaService.GetCinemaByAdminId(userId);

                if (tblCinema == null)
                {
                    return NotFound();
                }

                var numberOfTicketsSold = _context.TblTicket.Where(r => r.CinemaId == tblCinema.CinemaId && r.IsDeleted == false).Count();
                var moviesAvailable = _context.TblMovie.Where(r => r.CinemaId == tblCinema.CinemaId && r.IsDeleted == false).Count();

                CinemaDashboardViewModel cinemaDashboard = new CinemaDashboardViewModel();
                cinemaDashboard.NumberTicketsSold = numberOfTicketsSold;
                cinemaDashboard.MoviesAvailable = moviesAvailable;

                return View(cinemaDashboard);
            }
            catch (Exception ex)
            {
                return View("Index", null).WithSuccess("Error!", ex.Message);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetMonthStatistics()
        {
            try
            {
                var user = await GetCurrentUserAsync();
                var userId = user?.Id;

                var tblCinema = await _cinemaService.GetCinemaByAdminId(userId);

                if (tblCinema == null)
                {
                    return null;
                }

                int[] monthsStatsForMoviesRegistered = new int[3];
                int[] monthStatsForTicketsSold = new int[3];

                int currentMonthMoviesRegistered = 0;
                int secondMonthMoviesRegistered = 0;
                int thirdMonthMoviesRegistered = 0;

                int currentMonthTicketsSold = 0;
                int secondMonthTicketsSold = 0;
                int thirdMonthTicketsSold = 0;

                CinemaDashboardMonthlyStatsViewModel model = new CinemaDashboardMonthlyStatsViewModel();

                var ticketsSold = await _context.TblTicket.Where(r => r.CinemaId == tblCinema.CinemaId && r.IsDeleted == false).ToListAsync();
                var moviesRegistered = await _context.TblMovie.Where(r=>r.CinemaId == tblCinema.CinemaId && r.IsDeleted == false).ToListAsync();

                var currentmonth = DateTime.Now.ToString("MM");
                var secondMonth = DateTime.Now.AddMonths(-1).ToString("MM");
                var thirdMonth = DateTime.Now.AddMonths(-2).ToString("MM");

                foreach (var item in ticketsSold)
                {
                    string[] words = item.CreatedOnDate.Split('/');

                    if (!string.IsNullOrEmpty(words[1]))
                    {

                        if (words[1].Equals(currentmonth))
                        {
                            ++currentMonthTicketsSold;
                        }
                        else if (words[1].Equals(secondMonth))
                        {
                            ++secondMonthTicketsSold;
                        }
                        else if (words[1].Equals(thirdMonth))
                        {
                            ++thirdMonthTicketsSold;
                        }
                    }
                }

                monthStatsForTicketsSold[0] = currentMonthTicketsSold;
                monthStatsForTicketsSold[1] = secondMonthTicketsSold;
                monthStatsForTicketsSold[2] = thirdMonthTicketsSold;

                model.TicketsSold = monthStatsForTicketsSold;

                foreach (var item in moviesRegistered)
                {
                    string[] words = item.CreatedOnDate.Split('/');

                    if (!string.IsNullOrEmpty(words[1]))
                    {

                        if (words[1].Equals(currentmonth))
                        {
                            ++currentMonthMoviesRegistered;
                        }
                        else if (words[1].Equals(secondMonth))
                        {
                            ++secondMonthMoviesRegistered;
                        }
                        else if (words[1].Equals(thirdMonth))
                        {
                            ++thirdMonthMoviesRegistered;
                        }
                    }
                }

                monthsStatsForMoviesRegistered[0] = currentMonthMoviesRegistered;
                monthsStatsForMoviesRegistered[1] = secondMonthMoviesRegistered;
                monthsStatsForMoviesRegistered[2] = thirdMonthMoviesRegistered;

                model.MoviesRegistered = monthsStatsForMoviesRegistered;

                return Json(model);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCinema = await _cinemaService.GetCinemaById(id ?? 1);

            if (tblCinema == null)
            {
                return NotFound();
            }

            ViewData["AdminUserId"] = new SelectList(_context.AspNetUsers, "Id", "UserName", tblCinema.AdminUserId);
            ViewData["CountryId"] = new SelectList(_context.TblCountries, "CountryId", "CountryName", tblCinema.Adress.CountryId);
            ViewData["CityId"] = new SelectList(_context.TblCities, "CityId", "CityName", tblCinema.Adress.CityId);

            tblCinema.AdressId = tblCinema.Adress.AdressId;
            tblCinema.StreetName = tblCinema.Adress.StreetName;

            return View(tblCinema);
        }

        [HttpPost]
        public async Task<IActionResult> EditMyCinema(CinemaViewModel model)
        {
            var user = await GetCurrentUserAsync();
            var userId = user?.Id;
            string mail = user?.Email;

            model.LastModifiedByUserId = userId;
            model.AdminUserId = userId;

            var cinemaAdded = _cinemaService.EditCinema(model);

            if (cinemaAdded)
            {
                var tblCinema = await _cinemaService.GetCinemaByAdminId(userId);

                if (tblCinema == null)
                {
                    return NotFound();
                }

                var listOfAllMovies = _movieService.GetMoviesByCinemaId(tblCinema.CinemaId);
                ViewData["ListOfMovies"] = listOfAllMovies;

                return View("Index", tblCinema);
            }

            return BadRequest();
        }

        public IActionResult Add()
        {
            var showTimes = _context.TblShowTime.ToList();

            ViewData["LanguageId"] = new SelectList(_context.TblLanguage, "LanguageId", "LanguageName");
            ViewData["MovieGenreId"] = new SelectList(_context.TblMovieGenre, "MovieGenreId", "GenreDescription");
            ViewData["ShowTimes"] = showTimes;


            return View();
        }

        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            return await _imageHandler.UploadImage(file);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(MovieViewModel model)
        {
            var user = await GetCurrentUserAsync();
            var userId = user?.Id;
            string mail = user?.Email;

            var cinema = _context.TblCinema.Where(r => r.AdminUserId == userId).FirstOrDefault();

            model.CreatedByUserId = userId;
            model.LastModifiedByUserId = userId;
            model.CinemaId = cinema.CinemaId;

            var result = await UploadImage(model.Image);
            var test = result as ObjectResult;

            model.ImagePath = test.Value.ToString();


            var cinemaAdded = _movieService.AddMovie(model);

            if (cinemaAdded)
            {
                var tblCinema = await _cinemaService.GetCinemaByAdminId(userId);

                if (tblCinema == null)
                {
                    return NotFound();
                }

                var listOfAllMovies = _movieService.GetMoviesByCinemaId(tblCinema.CinemaId);
                ViewData["ListOfMovies"] = listOfAllMovies;

                return View("Index", tblCinema);
            }

            return BadRequest();
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}