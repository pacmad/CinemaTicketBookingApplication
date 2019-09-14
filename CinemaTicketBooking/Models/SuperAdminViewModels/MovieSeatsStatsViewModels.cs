using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTicketBooking.Models.SuperAdminViewModels
{
    public class MovieSeatsStatsViewModels
    {
        public List<string> BookedSeats { get; set; }
        public int CinemaSeatRows { get; set; }

        public int CinemaSeatColumns { get; set; }
    }
}
