using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemaTicketBooking.Entities;
using CinemaTicketBooking.Extensions;
using CinemaTicketBooking.Models;
using CinemaTicketBooking.Models.SuperAdminViewModels;
using CinemaTicketBooking.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CinemaTicketBooking.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class AdminDashboardController : Controller
    {
        private readonly ICinemaService _cinemaService;
        private readonly CinemaTicketBookingContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;

        public AdminDashboardController(CinemaTicketBookingContext context,
            UserManager<ApplicationUser> userManager,
            ILogger<AccountController> logger,
            ICinemaService cinemaService)
        {
            _userManager = userManager;
            _context = context;
            _logger = logger;
            _cinemaService = cinemaService;
        }

        // GET: AdminDashboard
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var numberOfActiveCinemas = _context.TblCinema.Where(r => r.IsDeleted == false).Count();
                var numberOfPassiveCinemas = _context.TblCinema.Where(r => r.IsDeleted == true).Count();
                var TicketsSold = _context.TblTicket.Count();
                var numberOfActiveMovies = _context.TblMovie.Where(r => r.IsDeleted == false).Count();
                var numberOfCinemas = _context.TblCinema.Count();
                var numberOfCustomers = await _userManager.GetUsersInRoleAsync("SimpleUser");

                AdminDashboardViewModel adminDashboard = new AdminDashboardViewModel();
                adminDashboard.NumberOfActiveCinemas = numberOfActiveCinemas;
                adminDashboard.NumberOfPassiveCinemas = numberOfPassiveCinemas;
                adminDashboard.NumberTicketsSold = TicketsSold;
                adminDashboard.ActiveMovies = numberOfActiveMovies;
                adminDashboard.CinemasRegistered = numberOfCinemas;
                adminDashboard.CustomersRegistered = numberOfCustomers.Count;

                return View(adminDashboard);
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

                int[] monthsStatsForCinemas = new int[3];
                int[] monthStatsForMovies = new int[3];

                int currentMonthCinemas = 0;
                int secondMonthCinemas = 0;
                int thirdMonthCinemas = 0;

                int currentMonthMovies = 0;
                int secondMonthMovies = 0;
                int thirdMonthMovies = 0;

                DashboardMonthlyStatsViewModel model = new DashboardMonthlyStatsViewModel();

                var cinemas = await _context.TblCinema.ToListAsync();
                var movies = await _context.TblMovie.ToListAsync();

                var currentmonth = DateTime.Now.ToString("MM");
                var secondMonth = DateTime.Now.AddMonths(-1).ToString("MM");
                var thirdMonth = DateTime.Now.AddMonths(-2).ToString("MM");

                foreach (var item in cinemas)
                {
                    string[] words = item.CreatedOnDate.Split('/');
                    if (words[1].Equals(currentmonth))
                    {
                        ++currentMonthCinemas;
                    }
                    else if (words[1].Equals(secondMonth))
                    {
                        ++secondMonthCinemas;
                    }
                    else if (words[1].Equals(thirdMonth))
                    {
                        ++thirdMonthCinemas;
                    }
                }

                monthsStatsForCinemas[0] = currentMonthCinemas;
                monthsStatsForCinemas[1] = secondMonthCinemas;
                monthsStatsForCinemas[2] = thirdMonthCinemas;

                model.CinemasRegistered = monthsStatsForCinemas;

                foreach (var item in movies)
                {
                    string[] words = item.CreatedOnDate.Split('/');

                    if (!string.IsNullOrEmpty(words[1]))
                    {

                        if (words[1].Equals(currentmonth))
                        {
                            ++currentMonthMovies;
                        }
                        else if (words[1].Equals(secondMonth))
                        {
                            ++secondMonthMovies;
                        }
                        else if (words[1].Equals(thirdMonth))
                        {
                            ++thirdMonthMovies;
                        }
                    }
                }

                monthStatsForMovies[0] = currentMonthMovies;
                monthStatsForMovies[1] = secondMonthMovies;
                monthStatsForMovies[2] = thirdMonthMovies;

                model.MoviesRegistered = monthStatsForMovies;

                return Json(model);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }
    }
}