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

        // GET: Cinemas
        public async Task<IActionResult> Index()
        {
            var cinemaTicketBookingContext = _context.TblCinema.Include(t => t.AdminUser).Include(t => t.Adress).Include(t => t.CreatedByUser).Include(t => t.LastModifiedByUser).Where(r=>r.IsDeleted == false);
            return View(await cinemaTicketBookingContext.ToListAsync());
        }

        // GET: Cinemas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCinema = await _context.TblCinema
                .Include(t => t.AdminUser)
                .Include(t => t.Adress)
                .Include(t => t.CreatedByUser)
                .Include(t => t.LastModifiedByUser)
                .SingleOrDefaultAsync(m => m.CinemaId == id);
            if (tblCinema == null)
            {
                return NotFound();
            }

            return View(tblCinema);
        }

        // GET: Cinemas/Create
        public IActionResult Create()
        {
            ViewData["AdminUserId"] = new SelectList(_context.AspNetUsers, "Id", "UserName");
            ViewData["AdressId"] = new SelectList(_context.TblAddress, "AdressId", "StreetName");
            ViewData["CreatedByUserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            ViewData["LastModifiedByUserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return View();
        }

        // POST: Cinemas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CinemaId,AdminUserId,CinemaName,CinemaDescription,CinemaProfilePicture,AdressId,CreatedByUserId,LastModifiedByUserId,CreatedOnDate,LastModifiedOnDate,IsDeleted")] TblCinema tblCinema)
        {
            var user = await GetCurrentUserAsync();
            var userId = user?.Id;
            string mail = user?.Email;

            tblCinema.CreatedByUserId = userId;
            tblCinema.LastModifiedByUserId = userId;
            tblCinema.CreatedOnDate = DateTime.Now.ToString("dd/MM/yyyy");
            tblCinema.LastModifiedOnDate = DateTime.Now.ToString("dd/MM/yyyy");
            tblCinema.IsDeleted = false;


            if (ModelState.IsValid)
            {
                _context.Add(tblCinema);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["AdminUserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", tblCinema.AdminUserId);
            ViewData["AdressId"] = new SelectList(_context.TblAddress, "AdressId", "CreatedByUserId", tblCinema.AdressId);
            ViewData["CreatedByUserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", userId);
            ViewData["LastModifiedByUserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", userId);
            return View(tblCinema);
        }

        // GET: Cinemas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCinema = await _context.TblCinema.SingleOrDefaultAsync(m => m.CinemaId == id);
            if (tblCinema == null)
            {
                return NotFound();
            }
            ViewData["AdminUserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", tblCinema.AdminUserId);
            ViewData["AdressId"] = new SelectList(_context.TblAddress, "AdressId", "CreatedByUserId", tblCinema.AdressId);
            ViewData["CreatedByUserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", tblCinema.CreatedByUserId);
            ViewData["LastModifiedByUserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", tblCinema.LastModifiedByUserId);
            return View(tblCinema);
        }

        // POST: Cinemas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CinemaId,AdminUserId,CinemaName,CinemaDescription,CinemaProfilePicture,AdressId,CreatedByUserId,LastModifiedByUserId,CreatedOnDate,LastModifiedOnDate,IsDeleted")] TblCinema tblCinema)
        {
            if (id != tblCinema.CinemaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblCinema);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblCinemaExists(tblCinema.CinemaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdminUserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", tblCinema.AdminUserId);
            ViewData["AdressId"] = new SelectList(_context.TblAddress, "AdressId", "CreatedByUserId", tblCinema.AdressId);
            ViewData["CreatedByUserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", tblCinema.CreatedByUserId);
            ViewData["LastModifiedByUserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", tblCinema.LastModifiedByUserId);
            return View(tblCinema);
        }

        // GET: Cinemas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var myCinema = await _cinemaService.GetCinemaById(id??0);

            if(myCinema != null)
            {
                return View(myCinema);
            }
            else
            {
                return NotFound();
            }

        }

        // POST: Cinemas/Delete/5
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
