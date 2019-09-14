using System;
using System.Collections.Generic;

namespace CinemaTicketBooking.Entities
{
    public partial class TblCinema
    {
        public TblCinema()
        {
            TblCustomerComments = new HashSet<TblCustomerComments>();
            TblMovie = new HashSet<TblMovie>();
            TblReservations = new HashSet<TblReservations>();
            TblShowTime = new HashSet<TblShowTime>();
            TblTicket = new HashSet<TblTicket>();
        }

        public int CinemaId { get; set; }
        public string AdminUserId { get; set; }
        public string CinemaName { get; set; }
        public string CinemaDescription { get; set; }
        public string CinemaProfilePicture { get; set; }
        public int AdressId { get; set; }
        public int? SeatsRows { get; set; }
        public int? SeatColumns { get; set; }
        public string CreatedByUserId { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string CreatedOnDate { get; set; }
        public string LastModifiedOnDate { get; set; }
        public bool IsDeleted { get; set; }

        public AspNetUsers AdminUser { get; set; }
        public TblAddress Adress { get; set; }
        public AspNetUsers CreatedByUser { get; set; }
        public AspNetUsers LastModifiedByUser { get; set; }
        public ICollection<TblCustomerComments> TblCustomerComments { get; set; }
        public ICollection<TblMovie> TblMovie { get; set; }
        public ICollection<TblReservations> TblReservations { get; set; }
        public ICollection<TblShowTime> TblShowTime { get; set; }
        public ICollection<TblTicket> TblTicket { get; set; }
    }
}
