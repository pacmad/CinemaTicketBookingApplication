using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemaTicketBooking.Entities;
using CinemaTicketBooking.Models;
using CinemaTicketBooking.Models.SuperAdminViewModels;
using CinemaTicketBooking.Repository;
using Microsoft.AspNetCore.Identity;

namespace CinemaTicketBooking.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly CinemaTicketBookingContext _context;

        public MovieService(IMovieRepository movieRepository,
            UserManager<ApplicationUser> userManager,
            IAddressRepository addressRepository,
            CinemaTicketBookingContext context)
        {
            _movieRepository = movieRepository;
            _userManager = userManager;
            _addressRepository = addressRepository;
            _context = context;
        }

        public bool AddMovie(MovieViewModel movie)
        {
            Images image = new Images()
            {
                ImagePath = movie.ImagePath
            };

            _context.Images.Add(image);

            if (_context.SaveChanges() > 0)
            {

                TblMovie tblMovie = new TblMovie()
                {
                    CinemaId = movie.CinemaId,
                    MovieGenreId = movie.MovieGenreId,
                    IsBookable = movie.IsBookable,
                    MovieName = movie.MovieName,
                    MovieDescription = movie.MovieDescription,
                    ReleaseDate = movie.ReleaseDate,
                    MovieLength = movie.MovieLength,
                    PriceForAdults = movie.PriceForAdults,
                    PriceForChildrens = movie.PriceForChildrens,
                    //ShowTimeIds = movie.ShowTimeIds,
                    Rating = movie.Rating,
                    LanguageId = movie.LanguageId,
                    Image = image.ImageId,
                    CreatedByUserId = movie.CreatedByUserId,
                    LastModifiedByUserId = movie.LastModifiedByUserId,
                    CreatedOnDate = movie.CreatedOnDate,
                    LastModifiedOnDate = movie.LastModifiedOnDate,
                    IsDeleted = movie.IsDeleted
                };

                var cinemaAdded = _movieRepository.AddMovie(tblMovie);

                if (cinemaAdded)
                {
                    TblShowTime showTime = new TblShowTime()
                    {
                        CinemaId = movie.CinemaId,
                        MovieId = tblMovie.MovieId,
                        Time = movie.ShowTime,
                        CreatedByUserId = movie.CreatedByUserId,
                        LastModifiedByUserId = movie.LastModifiedByUserId,
                        CreatedOnDate = DateTime.Now.ToShortDateString(),
                        LastModifiedOnDate = DateTime.Now.ToShortDateString(),
                    };

                    _context.TblShowTime.Add(showTime);

                    if (_context.SaveChanges() > 0)
                    {
                        var myMovie = _context.TblMovie.Where(r => r.MovieId == tblMovie.MovieId).SingleOrDefault();
                        myMovie.ShowTimeIds = showTime.ShowTimeId.ToString();
                        _movieRepository.EditMovie(myMovie);
                    }
                    return true;
                }
            }

            return false;
        }

        public Task<bool> DeleteMovie(int id)
        {
            var movieDeleted = _movieRepository.DeleteMovie(id);
            return Task.Run(() => movieDeleted);
        }

        public bool EditMovie(MovieViewModel movie)
        {
            var movieToEdit = _movieRepository.GetMovieById(movie.MovieId);
            movieToEdit.CinemaId = movie.CinemaId;
            movieToEdit.MovieName = movie.MovieName;
            movieToEdit.MovieDescription = movie.MovieDescription;
            movieToEdit.IsBookable = movie.IsBookable;
            movieToEdit.PriceForAdults = movie.PriceForAdults;
            movieToEdit.PriceForChildrens = movie.PriceForChildrens;
            movieToEdit.MovieLength = movie.MovieLength;
            movieToEdit.LastModifiedByUserId = movie.LastModifiedByUserId;
            movieToEdit.LastModifiedOnDate = DateTime.Now.ToString("dd/MM/yyyy");

            return _movieRepository.EditMovie(movieToEdit);
        }

        public IEnumerable<MovieViewModel> GetAllMovies()
        {
            var allMovies = _movieRepository.GetAllMovies();

            var targetList = allMovies
              .Select(x => new MovieViewModel()
              {

                  MovieId = x.MovieId,
                  CinemaId = x.CinemaId,
                  MovieGenreId = x.MovieGenreId,
                  IsBookable = x.IsBookable,
                  MovieName = x.MovieName,
                  MovieDescription = x.MovieDescription,
                  ReleaseDate = x.ReleaseDate,
                  MovieLength = x.MovieLength,
                  PriceForAdults = x.PriceForAdults,
                  PriceForChildrens = x.PriceForChildrens,
                  //ShowTimeIds = x.ShowTimeIds,
                  Rating = x.Rating,
                  LanguageId = x.LanguageId,
                  //Image = x.Image,
                  CreatedByUserId = x.CreatedByUserId,
                  LastModifiedByUserId = x.LastModifiedByUserId,
                  CreatedOnDate = x.CreatedOnDate,
                  LastModifiedOnDate = x.LastModifiedOnDate,
                  IsDeleted = x.IsDeleted,
                  Cinema = x.Cinema,
                  CreatedByUser = x.CreatedByUser,
                  ImageNavigation = x.ImageNavigation,
                  Language = x.Language,
                  LastModifiedByUser = x.LastModifiedByUser,
                  MovieGenre = x.MovieGenre,
                  TblCustomerComments = x.TblCustomerComments,
                  TblReservations = x.TblReservations,
                  TblShowTime = x.TblShowTime,
                  TblTicket = x.TblTicket,
              })
              .ToList();

            return targetList;
        }

        public IEnumerable<MovieViewModel> GetFilteredMovies(MoviesFilterViewModel model)
        {
            var allMovies = _movieRepository.GetFilteredMovies(model);

            var targetList = allMovies
              .Select(x => new MovieViewModel()
              {

                  MovieId = x.MovieId,
                  CinemaId = x.CinemaId,
                  MovieGenreId = x.MovieGenreId,
                  IsBookable = x.IsBookable,
                  MovieName = x.MovieName,
                  MovieDescription = x.MovieDescription,
                  ReleaseDate = x.ReleaseDate,
                  MovieLength = x.MovieLength,
                  PriceForAdults = x.PriceForAdults,
                  PriceForChildrens = x.PriceForChildrens,
                  //ShowTimeIds = x.ShowTimeIds,
                  Rating = x.Rating,
                  LanguageId = x.LanguageId,
                  //Image = x.Image,
                  CreatedByUserId = x.CreatedByUserId,
                  LastModifiedByUserId = x.LastModifiedByUserId,
                  CreatedOnDate = x.CreatedOnDate,
                  LastModifiedOnDate = x.LastModifiedOnDate,
                  IsDeleted = x.IsDeleted,
                  Cinema = x.Cinema,
                  CreatedByUser = x.CreatedByUser,
                  ImageNavigation = x.ImageNavigation,
                  Language = x.Language,
                  LastModifiedByUser = x.LastModifiedByUser,
                  MovieGenre = x.MovieGenre,
                  TblCustomerComments = x.TblCustomerComments,
                  TblReservations = x.TblReservations,
                  TblShowTime = x.TblShowTime,
                  TblTicket = x.TblTicket,
              })
              .ToList();

            return targetList;
        }

        public IEnumerable<MovieViewModel> GetMoviesByCinemaId(int id)
        {
            var movie = _movieRepository.GetMoviesByCinemaId(id);

            var targetList = movie
              .Select(x => new MovieViewModel()
              {

                  MovieId = x.MovieId,
                  CinemaId = x.CinemaId,
                  MovieGenreId = x.MovieGenreId,
                  IsBookable = x.IsBookable,
                  MovieName = x.MovieName,
                  MovieDescription = x.MovieDescription,
                  ReleaseDate = x.ReleaseDate,
                  MovieLength = x.MovieLength,
                  PriceForAdults = x.PriceForAdults,
                  PriceForChildrens = x.PriceForChildrens,
                  //ShowTimeIds = x.ShowTimeIds,
                  Rating = x.Rating,
                  LanguageId = x.LanguageId,
                  //Image = x.Image,
                  CreatedByUserId = x.CreatedByUserId,
                  LastModifiedByUserId = x.LastModifiedByUserId,
                  CreatedOnDate = x.CreatedOnDate,
                  LastModifiedOnDate = x.LastModifiedOnDate,
                  IsDeleted = x.IsDeleted,
                  Cinema = x.Cinema,
                  CreatedByUser = x.CreatedByUser,
                  ImageNavigation = x.ImageNavigation,
                  Language = x.Language,
                  LastModifiedByUser = x.LastModifiedByUser,
                  MovieGenre = x.MovieGenre,
                  TblCustomerComments = x.TblCustomerComments,
                  TblReservations = x.TblReservations,
                  TblShowTime = x.TblShowTime,
                  TblTicket = x.TblTicket,
              })
              .ToList();

            return targetList;
        }

        public Task<MovieViewModel> GetMovieById(int id)
        {
            var movie = _movieRepository.GetMovieById(id);

            MovieViewModel myCinema = new MovieViewModel()
            {
                MovieId = movie.MovieId,
                CinemaId = movie.CinemaId,
                MovieGenreId = movie.MovieGenreId,
                IsBookable = movie.IsBookable,
                MovieName = movie.MovieName,
                MovieDescription = movie.MovieDescription,
                ReleaseDate = movie.ReleaseDate,
                MovieLength = movie.MovieLength,
                PriceForAdults = movie.PriceForAdults,
                PriceForChildrens = movie.PriceForChildrens,
                ShowTime = movie.ShowTimeIds,
                Rating = movie.Rating,
                LanguageId = movie.LanguageId,
                CreatedByUserId = movie.CreatedByUserId,
                LastModifiedByUserId = movie.LastModifiedByUserId,
                CreatedOnDate = movie.CreatedOnDate,
                LastModifiedOnDate = movie.LastModifiedOnDate,
                IsDeleted = movie.IsDeleted,
                Cinema = movie.Cinema,
                CreatedByUser = movie.CreatedByUser,
                ImageNavigation = movie.ImageNavigation,
                Language = movie.Language,
                LastModifiedByUser = movie.LastModifiedByUser,
                MovieGenre = movie.MovieGenre,
                TblCustomerComments = movie.TblCustomerComments,
                TblReservations = movie.TblReservations,
                TblShowTime = movie.TblShowTime,
                TblTicket = movie.TblTicket,
            };
            return Task.Run(() => myCinema);
        }
    }
}
