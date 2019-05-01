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
    public class CinemaService : ICinemaService
    {
        private readonly ICinemaRepostiory _cinemaRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public CinemaService(ICinemaRepostiory cinemaRepository,
            UserManager<ApplicationUser> userManager)
        {
            _cinemaRepository = cinemaRepository;
            _userManager = userManager;
        }

        public bool AddCinema(CinemaViewModel cinema)
        {

            TblCinema tblCinema = new TblCinema()
            {
                CinemaId = cinema.CinemaId,
                AdminUserId = cinema.AdminUserId,
                CinemaName = cinema.CinemaName,
                CinemaDescription = cinema.CinemaDescription,
                AdressId = cinema.AdressId,
                CreatedByUserId = cinema.CreatedByUserId,
                LastModifiedByUserId = cinema.LastModifiedByUserId,
                CreatedOnDate = cinema.CreatedOnDate,
                LastModifiedOnDate = cinema.LastModifiedOnDate,
                AdminUser = cinema.AdminUser,
                Adress = cinema.Adress,
                CreatedByUser = cinema.CreatedByUser,
                LastModifiedByUser = cinema.LastModifiedByUser,
                IsDeleted = false
            };

            var cinemaAdded = _cinemaRepository.AddCinema(tblCinema);

            if (cinemaAdded)
            {
                return true;
            }

            return false;
        }

        public Task<bool> DeleteCinema(int id)
        {
            var cinemaDeleted = _cinemaRepository.DeleteCinema(id);
            return Task.Run(() => cinemaDeleted);
        }

        public bool EditCinema(CinemaViewModel cinema)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CinemaViewModel> GetAllCinemas()
        {
            throw new NotImplementedException();
        }

        public Task<CinemaViewModel> GetCinemaById(int id)
        {
            var cinema = _cinemaRepository.GetCinemaById(id);

            CinemaViewModel myCinema = new CinemaViewModel()
            {
                CinemaId = cinema.CinemaId,
                AdminUserId = cinema.AdminUserId,
                CinemaName = cinema.CinemaName,
                CinemaDescription = cinema.CinemaDescription,
                AdressId = cinema.AdressId,
                CreatedByUserId = cinema.CreatedByUserId,
                LastModifiedByUserId = cinema.LastModifiedByUserId,
                CreatedOnDate = cinema.CreatedOnDate,
                LastModifiedOnDate = cinema.LastModifiedOnDate,
                AdminUser = cinema.AdminUser,
                Adress = cinema.Adress,
                CreatedByUser = cinema.CreatedByUser,
                LastModifiedByUser = cinema.LastModifiedByUser,
                IsDeleted = false
            };
            return Task.Run(() => myCinema);

        }
    }
}
