using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTicketBooking.Models.SuperAdminViewModels
{
    public class CinemaDashboardViewModel
    {
        [Display(Name = "Movies available")]
        public int MoviesAvailable { get; set; }

        [Display(Name = "Tickets sold")]
        public int NumberTicketsSold { get; set; }
    }
}
