using System;
using System.Collections.Generic;

namespace CinemaTicketBooking.Entities
{
    public partial class TblCountries
    {
        public TblCountries()
        {
            TblAddress = new HashSet<TblAddress>();
            TblCities = new HashSet<TblCities>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CreatedByUserId { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string CreatedOnDate { get; set; }
        public string LastModifiedOnDate { get; set; }
        public bool IsDeleted { get; set; }

        public AspNetUsers CreatedByUser { get; set; }
        public AspNetUsers LastModifiedByUser { get; set; }
        public ICollection<TblAddress> TblAddress { get; set; }
        public ICollection<TblCities> TblCities { get; set; }
    }
}
