using System;
using System.Collections.Generic;

namespace CinemaTicketBooking.Entities
{
    public partial class TblMovie
    {
        public TblMovie()
        {
            TblCustomerComments = new HashSet<TblCustomerComments>();
            TblReservations = new HashSet<TblReservations>();
            TblShowTime = new HashSet<TblShowTime>();
            TblTicket = new HashSet<TblTicket>();
        }

        public int MovieId { get; set; }
        public int CinemaId { get; set; }
        public int? MovieGenreId { get; set; }
        public bool IsBookable { get; set; }
        public string MovieName { get; set; }
        public string MovieDescription { get; set; }
        public string ReleaseDate { get; set; }
        public string MovieLength { get; set; }
        public decimal PriceForAdults { get; set; }
        public decimal PriceForChildrens { get; set; }
        public string ShowTimeIds { get; set; }
        public double? Rating { get; set; }
        public int LanguageId { get; set; }
        public string CreatedByUserId { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string CreatedOnDate { get; set; }
        public string LastModifiedOnDate { get; set; }
        public bool IsDeleted { get; set; }

        public TblCinema Cinema { get; set; }
        public AspNetUsers CreatedByUser { get; set; }
        public TblLanguage Language { get; set; }
        public AspNetUsers LastModifiedByUser { get; set; }
        public TblMovieGenre MovieGenre { get; set; }
        public ICollection<TblCustomerComments> TblCustomerComments { get; set; }
        public ICollection<TblReservations> TblReservations { get; set; }
        public ICollection<TblShowTime> TblShowTime { get; set; }
        public ICollection<TblTicket> TblTicket { get; set; }
    }
}
