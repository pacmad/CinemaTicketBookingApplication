using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTicketBooking.Models.SuperAdminViewModels
{
    public class AdminDashboardViewModel
    {
        [Display(Name = "Active cinemas")]
        public int NumberOfActiveCinemas { get; set; }

        [Display(Name = "Passive cinemas")]
        public int NumberOfPassiveCinemas { get; set; }

        [Display(Name = "Number of tickets sold")]
        public int NumberTicketsSold { get; set; }

        [Display(Name = "Active movies")]
        public int ActiveMovies { get; set; }

        [Display(Name = "Customers registered")]
        public int CustomersRegistered { get; set; }

        [Display(Name = "Cinemas registered")]
        public int CinemasRegistered { get; set; }
    }
}
