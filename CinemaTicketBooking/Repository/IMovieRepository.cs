using CinemaTicketBooking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTicketBooking.Repository
{
    public interface IMovieRepository
    {
        TblMovie GetMovieById(int id);
        List<TblMovie> GetAllMovies();
        bool AddMovie(TblMovie movie);
        bool EditMovie(TblMovie movie);
        bool DeleteMovie(int id);
    }
}
