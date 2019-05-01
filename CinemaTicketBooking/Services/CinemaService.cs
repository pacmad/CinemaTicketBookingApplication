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
        private readonly IAddressRepository _addressRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public CinemaService(ICinemaRepostiory cinemaRepository,
            UserManager<ApplicationUser> userManager,
            IAddressRepository addressRepository)
        {
            _cinemaRepository = cinemaRepository;
            _userManager = userManager;
            _addressRepository = addressRepository;
        }

        public bool AddCinema(CinemaViewModel cinema)
        {

            TblAddress myAddress = new TblAddress()
            {
                CityId = cinema.CityId,
                CountryId = cinema.CountryId,
                StreetName = cinema.StreetName,
                CreatedByUserId = cinema.CreatedByUserId,
                LastModifiedByUserId = cinema.LastModifiedByUserId,
                CreatedOnDate = DateTime.Now.ToString("dd/mm/yyyy"),
                LastModifiedOnDate = DateTime.Now.ToString("dd/mm/yyyy"),
                IsDeleted = false
            };

            var addressAdded = _addressRepository.AddAddress(myAddress);

            if (addressAdded)
            {
                TblCinema tblCinema = new TblCinema()
                {
                    CinemaId = cinema.CinemaId,
                    AdminUserId = cinema.AdminUserId,
                    CinemaName = cinema.CinemaName,
                    CinemaDescription = cinema.CinemaDescription,
                    AdressId = myAddress.AdressId,
                    CreatedByUserId = cinema.CreatedByUserId,
                    LastModifiedByUserId = cinema.LastModifiedByUserId,
                    CreatedOnDate = DateTime.Now.ToString("dd/mm/yyyy"),
                    LastModifiedOnDate = DateTime.Now.ToString("dd/mm/yyyy"),
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
            var allCinemas = _cinemaRepository.GetAllCinemas();

            var targetList = allCinemas
              .Select(x => new CinemaViewModel() {
                  CinemaId = x.CinemaId,
                  AdminUserId = x.AdminUserId,
                  CinemaName = x.CinemaName,
                  CinemaDescription = x.CinemaDescription,
                  AdressId = x.AdressId,
                  CreatedByUserId = x.CreatedByUserId,
                  LastModifiedByUserId = x.LastModifiedByUserId,
                  CreatedOnDate = x.CreatedOnDate,
                  LastModifiedOnDate = x.LastModifiedOnDate,
                  AdminUser = x.AdminUser,
                  Adress = x.Adress,
                  CreatedByUser = x.CreatedByUser,
                  LastModifiedByUser = x.LastModifiedByUser
              })
              .ToList();

            return targetList;
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
