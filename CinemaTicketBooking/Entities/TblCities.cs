using System;
using System.Collections.Generic;

namespace CinemaTicketBooking.Entities
{
    public partial class TblCities
    {
        public TblCities()
        {
            TblAddress = new HashSet<TblAddress>();
        }

        public int CityId { get; set; }
        public string CityName { get; set; }
        public int CountryId { get; set; }
        public string CreatedByUserId { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string CreatedOnDate { get; set; }
        public string LastModifiedOnDate { get; set; }
        public bool IsDeleted { get; set; }

        public TblCountries Country { get; set; }
        public AspNetUsers CreatedByUser { get; set; }
        public AspNetUsers LastModifiedByUser { get; set; }
        public ICollection<TblAddress> TblAddress { get; set; }
    }
}
