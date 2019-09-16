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
        private readonly CinemaTicketBookingContext _context;

        public CinemaService(ICinemaRepostiory cinemaRepository,
            UserManager<ApplicationUser> userManager,
            IAddressRepository addressRepository,
            CinemaTicketBookingContext context)
        {
            _cinemaRepository = cinemaRepository;
            _userManager = userManager;
            _addressRepository = addressRepository;
            _context = context;
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
                CreatedOnDate = DateTime.Now.ToString("dd/MM/yyyy"),
                LastModifiedOnDate = DateTime.Now.ToString("dd/MM/yyyy"),
                IsDeleted = false
            };

            var addressAdded = _addressRepository.AddAddress(myAddress);

            if (addressAdded)
            {
                Images image = new Images()
                {
                    ImagePath = cinema.ImagePath
                };

                _context.Images.Add(image);

                TblCinema tblCinema = new TblCinema()
                {
                    CinemaId = cinema.CinemaId,
                    AdminUserId = cinema.AdminUserId,
                    CinemaName = cinema.CinemaName,
                    CinemaDescription = cinema.CinemaDescription,
                    AdressId = myAddress.AdressId,
                    CinemaProfilePicture = image.ImagePath,
                    CreatedByUserId = cinema.CreatedByUserId,
                    LastModifiedByUserId = cinema.LastModifiedByUserId,
                    CreatedOnDate = DateTime.Now.ToString("dd/MM/yyyy"),
                    LastModifiedOnDate = DateTime.Now.ToString("dd/MM/yyyy"),
                    AdminUser = cinema.AdminUser,
                    Adress = cinema.Adress,
                    SeatColumns = cinema.SeatColumns,
                    SeatsRows = cinema.SeatsRows,
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
            var cinemaToEdit = _cinemaRepository.GetCinemaById(cinema.CinemaId);
            cinemaToEdit.CinemaName = cinema.CinemaName;
            cinemaToEdit.CinemaDescription = cinema.CinemaDescription;
            cinemaToEdit.CinemaName = cinema.CinemaName;
            cinemaToEdit.LastModifiedByUserId = cinema.LastModifiedByUserId;
            cinemaToEdit.AdminUserId = cinema.AdminUserId;
            cinemaToEdit.LastModifiedOnDate = DateTime.Now.ToString("dd/MM/yyyy");

            return _cinemaRepository.EditCinema(cinemaToEdit);          
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

        public Task<CinemaViewModel> GetCinemaByAdminId(string id)
        {
            var cinema = _cinemaRepository.GetCinemaByAdminId(id);

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
                CinemaProfilePicture = cinema.CinemaProfilePicture,
                LastModifiedByUser = cinema.LastModifiedByUser,
                IsDeleted = false
            };
            return Task.Run(() => myCinema);

        }
    }
}
