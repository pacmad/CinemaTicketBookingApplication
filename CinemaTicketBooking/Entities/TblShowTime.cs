using System;
using System.Collections.Generic;

namespace CinemaTicketBooking.Entities
{
    public partial class TblShowTime
    {
        public int ShowTimeId { get; set; }
        public int CinemaId { get; set; }
        public int MovieId { get; set; }
        public string CreatedByUserId { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string CreatedOnDate { get; set; }
        public string LastModifiedOnDate { get; set; }
        public bool IsDeleted { get; set; }

        public TblCinema Cinema { get; set; }
        public AspNetUsers CreatedByUser { get; set; }
        public AspNetUsers LastModifiedByUser { get; set; }
        public TblMovie Movie { get; set; }
    }
}
