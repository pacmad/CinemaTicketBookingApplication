using CinemaTicketBooking.Models.SuperAdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTicketBooking.Services
{
    public interface ICinemaService
    {
        Task<CinemaViewModel> GetCinemaById(int id);
        Task<CinemaViewModel> GetCinemaByAdminId(string id);
        IEnumerable<CinemaViewModel> GetAllCinemas();
        bool AddCinema(CinemaViewModel cinema);
        bool EditCinema(CinemaViewModel cinema);
        Task<bool> DeleteCinema(int id);
    }
}
