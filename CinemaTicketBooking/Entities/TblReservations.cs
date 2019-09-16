using System;
using System.Collections.Generic;

namespace CinemaTicketBooking.Entities
{
    public partial class TblReservations
    {
        public int ReservationId { get; set; }
        public string ReservedByCustomerId { get; set; }
        public int ReservedForMovieId { get; set; }
        public int ReservedInCinemaId { get; set; }
        public string ReservationTime { get; set; }
        public bool IsPaid { get; set; }
        public int? PaymentTypeId { get; set; }
        public int? ReservationStatusId { get; set; }
        public string Seat { get; set; }
        public string CreatedByUserId { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string CreatedOnDate { get; set; }
        public string LastModifiedOnDate { get; set; }
        public bool IsDeleted { get; set; }

        public AspNetUsers CreatedByUser { get; set; }
        public AspNetUsers LastModifiedByUser { get; set; }
        public TblPaymentType PaymentType { get; set; }
        public TblReservationStatus ReservationStatus { get; set; }
        public AspNetUsers ReservedByCustomer { get; set; }
        public TblMovie ReservedForMovie { get; set; }
        public TblCinema ReservedInCinema { get; set; }
    }
}
