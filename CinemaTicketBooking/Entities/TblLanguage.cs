using System;
using System.Collections.Generic;

namespace CinemaTicketBooking.Entities
{
    public partial class TblLanguage
    {
        public TblLanguage()
        {
            TblMovie = new HashSet<TblMovie>();
        }

        public int LanguageId { get; set; }
        public string LanguageName { get; set; }
        public string CreatedByUserId { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string CreatedOnDate { get; set; }
        public string LastModifiedOnDate { get; set; }
        public bool IsDeleted { get; set; }

        public AspNetUsers CreatedByUser { get; set; }
        public AspNetUsers LastModifiedByUser { get; set; }
        public ICollection<TblMovie> TblMovie { get; set; }
    }
}
