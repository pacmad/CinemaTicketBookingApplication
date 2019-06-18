using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemaTicketBooking.Entities;
using CinemaTicketBooking.Models;
using CinemaTicketBooking.Models.SuperAdminViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketBooking.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly CinemaTicketBookingContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MovieRepository(CinemaTicketBookingContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public bool AddMovie(TblMovie movie)
        {
            _context.TblMovie.Add(movie);
            return _context.SaveChanges() > 0;
        }

        public bool DeleteMovie(int id)
        {
            var tblmovie = _context.TblMovie.SingleOrDefault(m => m.MovieId == id);
            tblmovie.IsDeleted = true;
            _context.TblMovie.Update(tblmovie);
            _context.Entry(tblmovie).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        }

        public bool EditMovie(TblMovie movie)
        {
            _context.TblMovie.Update(movie);
            _context.Entry(movie).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        }

        public List<TblMovie> GetAllMovies()
        {
            var cinemaTicketBookingContext = _context.TblMovie
                .Include(t => t.Cinema)
                .Include(t => t.CreatedByUser)
                .Include(t => t.ImageNavigation)
                .Include(t => t.Language)
                .Include(t => t.LastModifiedByUser)
                .Include(t => t.MovieGenre)
                .Include(t => t.TblCustomerComments)
                .Include(t => t.TblReservations)
                .Include(t => t.TblShowTime)
                .Include(t => t.TblTicket)
                .Where(r => r.IsDeleted == false).ToList();
            return cinemaTicketBookingContext;
        }

        public List<TblMovie> GetFilteredMovies(MoviesFilterViewModel model)
        {
            var result = _context.TblMovie.Include(t => t.Cinema)
                .Include(t => t.CreatedByUser)
                .Include(t => t.ImageNavigation)
                .Include(t => t.Language)
                .Include(t => t.LastModifiedByUser)
                .Include(t => t.Cinema)
                .ThenInclude(t => t.Adress)
                .Include(t => t.MovieGenre)
                .Include(t => t.TblCustomerComments)
                .Include(t => t.TblReservations)
                .Include(t => t.TblShowTime)
                .Include(t => t.TblTicket)
                .Where(r => r.IsDeleted == false).AsQueryable();

            if (model != null)
            {
                if (model.MovieGenreId.HasValue)
                {
                    result = result.Where(x => x.MovieGenreId == model.MovieGenreId);
                }
                if (model.CountryId.HasValue)
                {
                    result = result.Where(x => x.Cinema.Adress.CountryId == model.CountryId);
                }
                if (model.CityId.HasValue)
                {
                    result = result.Where(x => x.Cinema.Adress.CityId == model.CityId);
                }
                if (model.CinemaId.HasValue)
                {
                    result = result.Where(x => x.CinemaId == model.CinemaId);
                }
                if (model.LanguageId.HasValue)
                {
                    result = result.Where(x => x.LanguageId == model.LanguageId);
                }
            }
            return result.ToList();
        }

        public TblMovie GetMovieById(int id)
        {
            var tblMobie = _context.TblMovie
               .Include(t => t.Cinema)
               .Include(t => t.CreatedByUser)
               .Include(t => t.ImageNavigation)
               .Include(t => t.Language)
               .Include(t => t.LastModifiedByUser)
               .Include(t => t.MovieGenre)
               .Include(t => t.TblCustomerComments)
               .Include(t => t.TblReservations)
               .Include(t => t.TblShowTime)
               .Include(t => t.TblTicket)
               .Where(r => r.IsDeleted == false && r.MovieId == id)
               .SingleOrDefault();

            return tblMobie;
        }

        public List<TblMovie> GetMoviesByCinemaId(int id)
        {
            var tblMobie = _context.TblMovie
               .Include(t => t.Cinema)
               .Include(t => t.CreatedByUser)
               .Include(t => t.ImageNavigation)
               .Include(t => t.Language)
               .Include(t => t.LastModifiedByUser)
               .Include(t => t.MovieGenre)
               .Include(t => t.TblCustomerComments)
               .Include(t => t.TblReservations)
               .Include(t => t.TblShowTime)
               .Include(t => t.TblTicket)
               .Where(r => r.IsDeleted == false && r.CinemaId == id)
               .ToList();

            return tblMobie;
        }
    }
}
