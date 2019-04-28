using System;
using System.Collections.Generic;

namespace CinemaTicketBooking.Entities
{
    public partial class TblSeat
    {
        public TblSeat()
        {
            TblTicket = new HashSet<TblTicket>();
        }

        public int SeatId { get; set; }
        public string SeatNumber { get; set; }
        public int CinemaId { get; set; }
        public string CreatedByUserId { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string CreatedOnDate { get; set; }
        public string LastModifiedOnDate { get; set; }
        public bool IsDeleted { get; set; }

        public TblCinema Cinema { get; set; }
        public ICollection<TblTicket> TblTicket { get; set; }
    }
}
