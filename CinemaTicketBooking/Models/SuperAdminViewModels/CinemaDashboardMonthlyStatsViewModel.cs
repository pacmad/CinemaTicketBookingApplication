using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTicketBooking.Models.SuperAdminViewModels
{
    public class CinemaDashboardMonthlyStatsViewModel
    {
        public int[] MoviesRegistered { get; set; }
        public int[] TicketsSold { get; set; }
    }
}
