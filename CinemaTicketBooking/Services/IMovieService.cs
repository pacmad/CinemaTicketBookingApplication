using CinemaTicketBooking.Models.SuperAdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTicketBooking.Services
{
    public interface IMovieService
    {
        Task<MovieViewModel> GetMovieById(int id);
        IEnumerable<MovieViewModel> GetMoviesByCinemaId(int id);
        IEnumerable<MovieViewModel> GetAllMovies();
        bool AddMovie(MovieViewModel movie);
        bool EditMovie(MovieViewModel movie);
        Task<bool> DeleteMovie(int id);
    }
}
