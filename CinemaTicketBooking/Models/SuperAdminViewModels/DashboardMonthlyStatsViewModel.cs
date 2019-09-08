using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTicketBooking.Models.SuperAdminViewModels
{
    public class DashboardMonthlyStatsViewModel
    {
        public int[] CinemasRegistered { get; set; }
        public int[] MoviesRegistered { get; set; }
    }
}
