using CinemaTicketBooking.Entities;
using CinemaTicketBooking.Extensions;
using CinemaTicketBooking.Models;
using CinemaTicketBooking.Models.SuperAdminViewModels;
using CinemaTicketBooking.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTicketBooking.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ICinemaService _cinemaService;
        private readonly CinemaTicketBookingContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;
        private readonly IImageHandler _imageHandler;

        public CinemasController(CinemaTicketBookingContext context,
            UserManager<ApplicationUser> userManager,
            ILogger<AccountController> logger,
            ICinemaService cinemaService,
            IImageHandler imageHandler)
        {
            _userManager = userManager;
            _context = context;
            _logger = logger;
            _cinemaService = cinemaService;
            _imageHandler = imageHandler;
        }

        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            return await _imageHandler.UploadImage(file);
        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Index()
        {
            var listOfAllCinemas = _cinemaService.GetAllCinemas();

            foreach (var item in listOfAllCinemas)
            {
                var newdescription = item.CinemaDescription.Length <= 40 ? item.CinemaDescription : item.CinemaDescription.Substring(0, 40) + "...";
                item.CinemaDescription = newdescription;
            }

            return View(listOfAllCinemas.ToList());
        }

        [Authorize(Roles = "SuperAdmin")]
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

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            List<string> userids = _context.AspNetUserRoles.Where(a => a.RoleId == "1").Select(b => b.UserId).Distinct().ToList();

            List<AspNetUsers> listUsers = _context.AspNetUsers.Where(a => userids.Any(c => c == a.Id)).ToList();

            ViewData["AdminUserId"] = new SelectList(listUsers, "Id", "UserName");
            ViewData["CountryId"] = new SelectList(_context.TblCountries, "CountryId", "CountryName");
            ViewData["CityId"] = new SelectList(_context.TblCities, "CityId", "CityName");

            return View();
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CinemaViewModel model)
        {
            var user = await GetCurrentUserAsync();
            var userId = user?.Id;
            string mail = user?.Email;

            model.CreatedByUserId = userId;
            model.LastModifiedByUserId = userId;

            var result = await UploadImage(model.Image);
            var test = result as ObjectResult;

            model.ImagePath = test.Value.ToString();

            var cinemaAdded = _cinemaService.AddCinema(model);


            if (cinemaAdded)
            {
                var listOfAllCinemas = _cinemaService.GetAllCinemas();
                return View("Index", listOfAllCinemas.ToList()).WithSuccess("Info!", "Cinema was created successfully!");
            }

            return BadRequest();
        }

        [Authorize(Roles = "SuperAdmin")]
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

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CinemaViewModel model)
        {
            if (id != model.CinemaId)
            {
                return NotFound();
            }
            var user = await GetCurrentUserAsync();
            var userId = user?.Id;
            string mail = user?.Email;

            model.LastModifiedByUserId = userId;

            var cinemaAdded = _cinemaService.EditCinema(model);

            if (cinemaAdded)
            {
                var listOfAllCinemas = _cinemaService.GetAllCinemas();
                return View("Index", listOfAllCinemas.ToList()).WithSuccess("Info!", "Cinema was edited successfully!");
            }

            else
            {
                return View(model);
            }
        }

        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
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

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cinemaDeletd = await _cinemaService.DeleteCinema(id);

            if (cinemaDeletd)
            {
                var listOfAllCinemas = _cinemaService.GetAllCinemas();
                return View("Index", listOfAllCinemas.ToList()).WithSuccess("Info!", "Cinema was deleted successfully!");
            }
            else
            {
                return BadRequest();
            }

        }

        [Authorize(Roles = "SuperAdmin")]
        private bool TblCinemaExists(int id)
        {
            return _context.TblCinema.Any(e => e.CinemaId == id);
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
