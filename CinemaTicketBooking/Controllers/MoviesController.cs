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

namespace CinemaTicketBooking.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly CinemaTicketBookingContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;

        public MoviesController(CinemaTicketBookingContext context,
            UserManager<ApplicationUser> userManager,
            ILogger<AccountController> logger,
            IMovieService movieService)
        {
            _userManager = userManager;
            _context = context;
            _logger = logger;
            _movieService = movieService;
        }

        [Authorize(Roles = "SuperAdmin")]
        [Authorize(Roles = "CinemaAdmin")]
        public IActionResult Index()
        {
            var listOfAllMovies = _movieService.GetAllMovies();
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
            ViewData["CinemaId"] = new SelectList(_context.TblCinema, "CinemaId", "AdminUserId");
            ViewData["Image"] = new SelectList(_context.Images, "ImageId", "ImagePath");
            ViewData["LanguageId"] = new SelectList(_context.TblLanguage, "LanguageId", "LanguageName");
            ViewData["MovieGenreId"] = new SelectList(_context.TblMovieGenre, "MovieGenreId", "GenreDescription");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieViewModel model)
        {
            var user = await GetCurrentUserAsync();
            var userId = user?.Id;
            string mail = user?.Email;

            var cinema = await _context.TblCinema.Where(r => r.AdminUserId == userId).SingleOrDefaultAsync();

            model.CreatedByUserId = userId;
            model.LastModifiedByUserId = userId;
            model.CinemaId = cinema.CinemaId;

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var cinemaAdded = _movieService.AddMovie(model);


            if (cinemaAdded)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest();
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblMovie = await _context.TblMovie.SingleOrDefaultAsync(m => m.MovieId == id);
            if (tblMovie == null)
            {
                return NotFound();
            }
            ViewData["CinemaId"] = new SelectList(_context.TblCinema, "CinemaId", "AdminUserId", tblMovie.CinemaId);
            ViewData["CreatedByUserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", tblMovie.CreatedByUserId);
            ViewData["Image"] = new SelectList(_context.Images, "ImageId", "ImagePath", tblMovie.Image);
            ViewData["LanguageId"] = new SelectList(_context.TblLanguage, "LanguageId", "LanguageName", tblMovie.LanguageId);
            ViewData["LastModifiedByUserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", tblMovie.LastModifiedByUserId);
            ViewData["MovieGenreId"] = new SelectList(_context.TblMovieGenre, "MovieGenreId", "GenreDescription", tblMovie.MovieGenreId);
            return View(tblMovie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieId,CinemaId,MovieGenreId,IsBookable,MovieName,MovieDescription,ReleaseDate,MovieLength,PriceForAdults,PriceForChildrens,ShowTimeIds,Rating,LanguageId,Image,CreatedByUserId,LastModifiedByUserId,CreatedOnDate,LastModifiedOnDate,IsDeleted")] TblMovie tblMovie)
        {
            if (id != tblMovie.MovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblMovie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblMovieExists(tblMovie.MovieId))
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
            ViewData["CinemaId"] = new SelectList(_context.TblCinema, "CinemaId", "AdminUserId", tblMovie.CinemaId);
            ViewData["CreatedByUserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", tblMovie.CreatedByUserId);
            ViewData["Image"] = new SelectList(_context.Images, "ImageId", "ImagePath", tblMovie.Image);
            ViewData["LanguageId"] = new SelectList(_context.TblLanguage, "LanguageId", "LanguageName", tblMovie.LanguageId);
            ViewData["LastModifiedByUserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", tblMovie.LastModifiedByUserId);
            ViewData["MovieGenreId"] = new SelectList(_context.TblMovieGenre, "MovieGenreId", "GenreDescription", tblMovie.MovieGenreId);
            return View(tblMovie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblMovie = await _context.TblMovie
                .Include(t => t.Cinema)
                .Include(t => t.CreatedByUser)
                .Include(t => t.ImageNavigation)
                .Include(t => t.Language)
                .Include(t => t.LastModifiedByUser)
                .Include(t => t.MovieGenre)
                .SingleOrDefaultAsync(m => m.MovieId == id);
            if (tblMovie == null)
            {
                return NotFound();
            }

            return View(tblMovie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblMovie = await _context.TblMovie.SingleOrDefaultAsync(m => m.MovieId == id);
            _context.TblMovie.Remove(tblMovie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblMovieExists(int id)
        {
            return _context.TblMovie.Any(e => e.MovieId == id);
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
