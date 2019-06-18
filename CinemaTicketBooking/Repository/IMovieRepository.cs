using CinemaTicketBooking.Entities;
using CinemaTicketBooking.Models.SuperAdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTicketBooking.Repository
{
    public interface IMovieRepository
    {
        TblMovie GetMovieById(int id);
        List<TblMovie> GetMoviesByCinemaId(int id);
        List<TblMovie> GetAllMovies();
        List<TblMovie> GetFilteredMovies(MoviesFilterViewModel model);
        bool AddMovie(TblMovie movie);
        bool EditMovie(TblMovie movie);
        bool DeleteMovie(int id);
    }
}
