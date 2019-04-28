﻿using System;
using System.Collections.Generic;

namespace CinemaTicketBooking.Entities
{
    public partial class TblTicket
    {
        public int TicketId { get; set; }
        public string CustomerId { get; set; }
        public int CinemaId { get; set; }
        public int MovieId { get; set; }
        public int SeatId { get; set; }
        public string Price { get; set; }
        public string TotalPrice { get; set; }
        public string CreatedByUserId { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string CreatedOnDate { get; set; }
        public string LastModifiedOnDate { get; set; }
        public bool IsDeleted { get; set; }

        public TblCinema Cinema { get; set; }
        public AspNetUsers CreatedByUser { get; set; }
        public AspNetUsers Customer { get; set; }
        public AspNetUsers LastModifiedByUser { get; set; }
        public TblMovie Movie { get; set; }
        public TblSeat Seat { get; set; }
    }
}
