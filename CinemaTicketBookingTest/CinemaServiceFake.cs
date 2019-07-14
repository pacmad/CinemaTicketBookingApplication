using CinemaTicketBooking.Models.SuperAdminViewModels;
using CinemaTicketBooking.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace CinemaTicketBookingTest
{
    public class CinemaServiceFake : ICinemaService
    {
        private readonly List<CinemaViewModel> _listOfCinemas;

        public CinemaServiceFake()
        {
            _listOfCinemas = new List<CinemaViewModel>()
            {
                new CinemaViewModel()
                {
                    CinemaId = 55,
                    AdminUserId = "fa80f392-58b4-4415-ba40-c118c0651801",
                    CinemaName = "CINEPLEXX",
                    CinemaDescription = "CINEPLEXX PRISHTINA OFFICIAL",
                    AdressId = 1,
                    CreatedByUserId = "fa80f392-58b4-4415-ba40-c118c0651801",
                    LastModifiedByUserId = "fa80f392-58b4-4415-ba40-c118c0651801",
                    CreatedOnDate = "26/34/2019",
                    LastModifiedOnDate = "26/34/2019",
                    IsDeleted = false
                },
               new CinemaViewModel()
                {
                    CinemaId = 56,
                    AdminUserId = "fa80f392-58b4-4415-ba40-c118c0651801",
                    CinemaName = "ABC",
                    CinemaDescription = "ABC PRISHTINA OFFICIAL",
                    AdressId = 2,
                    CreatedByUserId = "fa80f392-58b4-4415-ba40-c118c0651801",
                    LastModifiedByUserId = "fa80f392-58b4-4415-ba40-c118c0651801",
                    CreatedOnDate = "26/34/2019",
                    LastModifiedOnDate = "26/34/2019",
                    IsDeleted = false
                },
            };
        }

        public CinemaViewModel GetCinemaById(int id)
        {
            return _listOfCinemas.Where(a => a.CinemaId == id)
                .FirstOrDefault();
        }

        public IEnumerable<CinemaViewModel> GetAllCinemas()
        {
            return _listOfCinemas;
        }

        public bool AddCinema(CinemaViewModel cinema)
        {
            _listOfCinemas.Add(cinema);
            return true;
        }

        public Task<bool> DeleteCinema(int id)
        {
            var existing = _listOfCinemas.First(a => a.CinemaId == id);
            _listOfCinemas.Remove(existing);
            return Task.Run(() => true);
        }

        Task<CinemaViewModel> ICinemaService.GetCinemaById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CinemaViewModel> GetCinemaByAdminId(string id)
        {
            throw new NotImplementedException();
        }

        public bool EditCinema(CinemaViewModel cinema)
        {
            throw new NotImplementedException();
        }
    }
}

