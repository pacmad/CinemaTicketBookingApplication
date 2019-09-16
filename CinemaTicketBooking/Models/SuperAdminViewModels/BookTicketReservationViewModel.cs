using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTicketBooking.Models.SuperAdminViewModels
{
    public class BookTicketReservationViewModel
    {
        public int? movieId { get; set; }
        public SeatReservationViewModel[] selectedSeats { get; set; }
    }
}
