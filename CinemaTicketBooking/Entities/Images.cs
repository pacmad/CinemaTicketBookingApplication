using System;
using System.Collections.Generic;

namespace CinemaTicketBooking.Entities
{
    public partial class Images
    {
        public Images()
        {
            TblMovie = new HashSet<TblMovie>();
        }

        public int ImageId { get; set; }
        public string ImagePath { get; set; }

        public ICollection<TblMovie> TblMovie { get; set; }
    }
}
