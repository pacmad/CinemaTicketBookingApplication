using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTicketBooking.Models.SuperAdminViewModels
{
    public class MoviesFilterViewModel
    {
        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public int? MovieGenreId { get; set; }
        public int? CinemaId { get; set; }
        public int? LanguageId { get; set; }
    }
}
