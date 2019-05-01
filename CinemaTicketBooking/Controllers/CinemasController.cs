using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaTicketBooking.Entities;
using CinemaTicketBooking.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using CinemaTicketBooking.Services;
using CinemaTicketBooking.Models.SuperAdminViewModels;

namespace CinemaTicketBooking.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ICinemaService _cinemaService;
        private readonly CinemaTicketBookingContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;

        public CinemasController(CinemaTicketBookingContext context,
            UserManager<ApplicationUser> userManager,
            ILogger<AccountController> logger,
            ICinemaService cinemaService)
        {
            _userManager = userManager;
            _context = context;
            _logger = logger;
            _cinemaService = cinemaService;
        }

        public IActionResult Index()
        {
            var listOfAllCinemas = _cinemaService.GetAllCinemas();
            return View(listOfAllCinemas.ToList());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myCinema = await _cinemaService.GetCinemaById(id ?? 1);

            if (myCinema == null)
            {
                return NotFound();
            }

            return View(myCinema);

        }

        public IActionResult Create()
        {
            ViewData["AdminUserId"] = new SelectList(_context.AspNetUsers, "Id", "UserName");
            ViewData["CountryId"] = new SelectList(_context.TblCountries, "CountryId", "CountryName");
            ViewData["CityId"] = new SelectList(_context.TblCities, "CityId", "CityName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CinemaViewModel model)
        {
            var user = await GetCurrentUserAsync();
            var userId = user?.Id;
            string mail = user?.Email;

            model.CreatedByUserId = userId;
            model.LastModifiedByUserId = userId;

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var cinemaAdded = _cinemaService.AddCinema(model);


            if (cinemaAdded)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest();
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CinemaViewModel model)
        {
            if (id != model.CinemaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var user = await GetCurrentUserAsync();
                var userId = user?.Id;
                string mail = user?.Email;

                model.LastModifiedByUserId = userId;

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var cinemaAdded = _cinemaService.EditCinema(model);

                if (cinemaAdded)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            else
            {
                return View(model);
            }

            return BadRequest();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var myCinema = await _cinemaService.GetCinemaById(id ?? 0);

            if (myCinema != null)
            {
                return View(myCinema);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cinemaDeletd = await _cinemaService.DeleteCinema(id);

            if (cinemaDeletd)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return BadRequest();
            }

        }

        private bool TblCinemaExists(int id)
        {
            return _context.TblCinema.Any(e => e.CinemaId == id);
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
