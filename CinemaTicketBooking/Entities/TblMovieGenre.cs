using System;
using System.Collections.Generic;

namespace CinemaTicketBooking.Entities
{
    public partial class TblMovieGenre
    {
        public TblMovieGenre()
        {
            TblMovie = new HashSet<TblMovie>();
        }

        public int MovieGenreId { get; set; }
        public string MovieGenreName { get; set; }
        public string GenreDescription { get; set; }
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
