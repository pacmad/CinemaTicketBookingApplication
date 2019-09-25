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
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace CinemaTicketBooking.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly CinemaTicketBookingContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;
        private readonly IImageHandler _imageHandler;

        public HomeController(CinemaTicketBookingContext context,
            IMovieService movieService,
            UserManager<ApplicationUser> userManager,
            ILogger<AccountController> logger,
            IImageHandler imageHandler)
        {
            _userManager = userManager;
            _context = context;
            _logger = logger;
            _movieService = movieService;
            _imageHandler = imageHandler;
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

                foreach (var item in movies)
                {
                    var newdescription = item.MovieDescription.Length <= 60 ? item.MovieDescription : item.MovieDescription.Substring(0, 60) + "...";
                    item.MovieDescription = newdescription;
                }


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

        public async Task<IActionResult> MyTickets()
        {
            var user = await GetCurrentUserAsync();

            if (user == null)
            {
                return BadRequest("There is no logged in user.");
            }

            var userTickets = await _context.TblTicket
                .Include(t => t.Cinema)
                .Include(t => t.Movie)
                .ThenInclude(t => t.TblShowTime)
                .Where(r => r.CustomerId == user.Id && r.IsDeleted == false).ToListAsync();

            return View(userTickets);
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> MyBookings()
        {

            try
            {
                var user = await GetCurrentUserAsync();

                if (user == null)
                {
                    return BadRequest("There is no logged in user.");
                }

                var reservations = await _context.TblReservations
                    .Include(t => t.ReservedInCinema)
                    .Include(t => t.ReservedForMovie)
                    .ThenInclude(t => t.ImageNavigation)
                    .Where(r => r.ReservedByCustomerId == user.Id
                    && r.IsPaid == false
                    && r.ReservationStatusId == 2
                    && r.IsDeleted == false).ToListAsync();

                if (reservations == null)
                {
                    return BadRequest("Could not get reservations for user.");
                }

                decimal totalPrice = 0;

                foreach (var item in reservations)
                {
                    totalPrice += Convert.ToDecimal(item.ReservedForMovie.PriceForAdults);
                }

                ViewData["TotalPrice"] = totalPrice.ToString();

                return View(reservations);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveReservation([FromBody]int reservationId)
        {
            var reservation = await _context.TblReservations.Where(r => r.ReservationId == reservationId).FirstOrDefaultAsync();

            if (reservation != null)
            {
                reservation.IsDeleted = true;
                _context.TblReservations.Update(reservation);
                _context.Entry(reservation).State = EntityState.Modified;

                if (_context.SaveChanges() > 0)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Could not delete reservation");
                }
            }
            else
            {
                return BadRequest("Could not find reservation");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTicket(int? TicketId)
        {
            var user = await GetCurrentUserAsync();

            if (user == null)
            {
                return BadRequest("There is no logged in user.");
            }

            var userTickets = await _context.TblTicket
            .Include(t => t.Cinema)
            .Include(t => t.Movie)
            .ThenInclude(t => t.TblShowTime)
            .Where(r => r.CustomerId == user.Id && r.IsDeleted == false).ToListAsync();

            var ticket = await _context.TblTicket.Where(r => r.TicketId == TicketId && r.CustomerId == user.Id).FirstOrDefaultAsync();

            if (ticket != null)
            {
                ticket.IsDeleted = true;
                _context.TblTicket.Update(ticket);
                _context.Entry(ticket).State = EntityState.Modified;

                if (_context.SaveChanges() > 0)
                {
                    return View("MyTickets", userTickets).WithSuccess("Info!", "Ticket was deleted successfully!");
                }
                else
                {
                    return View("MyTickets", userTickets).WithDanger("Info!", "Ticket could not be deleted!");
                }
            }
            else
            {
                return View("MyTickets", userTickets).WithDanger("Info!", "Ticket could not be deleted!");
            }

        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {

            try
            {
                var user = await GetCurrentUserAsync();

                if (user == null)
                {
                    return BadRequest("There is no logged in user.");
                }

                var reservations = await _context.TblReservations
                    .Include(t => t.ReservedInCinema)
                    .Include(t => t.ReservedForMovie)
                    .ThenInclude(t => t.ImageNavigation)
                    .Where(r => r.ReservedByCustomerId == user.Id
                    && r.IsPaid == false
                    && r.ReservationStatusId == 2
                    && r.IsDeleted == false).ToListAsync();

                if (reservations == null)
                {
                    return BadRequest("Could not get reservations for user.");
                }

                decimal totalPrice = 0;

                foreach (var item in reservations)
                {
                    totalPrice += Convert.ToDecimal(item.ReservedForMovie.PriceForAdults);
                }

                ViewData["TotalPrice"] = totalPrice.ToString();

                return View(reservations);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public async Task<IActionResult> PayTickets()
        {
            try
            {
                var user = await GetCurrentUserAsync();

                if (user == null)
                {
                    return BadRequest("There is no logged in user.");
                }

                var reservations = await _context.TblReservations
                    .Include(t => t.ReservedInCinema)
                    .Include(t => t.ReservedForMovie)
                    .ThenInclude(t => t.ImageNavigation)
                    .Where(r => r.ReservedByCustomerId == user.Id
                    && r.IsPaid == false
                    && r.ReservationStatusId == 2
                    && r.IsDeleted == false).ToListAsync();

                if (reservations == null)
                {
                    return BadRequest("Could not get reservations for user.");
                }

                decimal totalPrice = 0;

                foreach (var item in reservations)
                {
                    totalPrice += Convert.ToDecimal(item.ReservedForMovie.PriceForAdults);
                }

                ViewData["TotalPrice"] = totalPrice.ToString();

                foreach (var item in reservations)
                {
                    item.IsPaid = true;
                    item.PaymentTypeId = 2;
                    item.ReservationStatusId = 1;
                    _context.TblReservations.Update(item);
                    _context.Entry(item).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    TblTicket ticket = new TblTicket();
                    ticket.CustomerId = user.Id;
                    ticket.CinemaId = item.ReservedInCinema.CinemaId;
                    ticket.MovieId = item.ReservedForMovie.MovieId;
                    ticket.Seat = item.Seat;
                    ticket.Price = Convert.ToDecimal(item.ReservedForMovie.PriceForAdults);
                    ticket.TotalPrice = Convert.ToDecimal(item.ReservedForMovie.PriceForAdults);
                    ticket.CreatedByUserId = user.Id;
                    ticket.LastModifiedByUserId = user.Id;
                    ticket.CreatedOnDate = DateTime.Now.ToShortDateString();
                    ticket.LastModifiedOnDate = DateTime.Now.ToShortDateString();

                    await _context.TblTicket.AddAsync(ticket);
                    await _context.SaveChangesAsync();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
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

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

    }
}
