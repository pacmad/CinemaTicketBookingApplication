using System;
using System.Collections.Generic;

namespace CinemaTicketBooking.Entities
{
    public partial class TblPaymentType
    {
        public TblPaymentType()
        {
            TblReservations = new HashSet<TblReservations>();
        }

        public int PaymentTypeId { get; set; }
        public string PaymentTypeName { get; set; }
        public string CreatedByUserId { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string CreatedOnDate { get; set; }
        public string LastModifiedOnDate { get; set; }
        public bool IsDeleted { get; set; }

        public AspNetUsers CreatedByUser { get; set; }
        public AspNetUsers LastModifiedByUser { get; set; }
        public ICollection<TblReservations> TblReservations { get; set; }
    }
}
