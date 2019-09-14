using System;
using System.Collections.Generic;

namespace CinemaTicketBooking.Entities
{
    public partial class TblAddress
    {
        public TblAddress()
        {
            TblCinema = new HashSet<TblCinema>();
        }

        public int AdressId { get; set; }
        public int CityId { get; set; }
        public int CountryId { get; set; }
        public string StreetName { get; set; }
        public int? FlatNumber { get; set; }
        public string CreatedByUserId { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string CreatedOnDate { get; set; }
        public string LastModifiedOnDate { get; set; }
        public bool IsDeleted { get; set; }

        public TblCities City { get; set; }
        public TblCountries Country { get; set; }
        public AspNetUsers CreatedByUser { get; set; }
        public AspNetUsers LastModifiedByUser { get; set; }
        public ICollection<TblCinema> TblCinema { get; set; }
    }
}
