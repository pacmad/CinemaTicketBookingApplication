using CinemaTicketBooking.Entities;
using CinemaTicketBooking.Models.SuperAdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTicketBooking.Repository
{
    public interface ICinemaRepostiory
    {
        TblCinema GetCinemaById(int id);
        List<TblCinema> GetAllCinemas();
        bool AddCinema(TblCinema cinema);
        bool EditCinema(TblCinema cinema);
        bool DeleteCinema(int id);
    }
}
