using CinemaTicketBooking.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTicketBooking.Models.SuperAdminViewModels
{
    public class MovieViewModel
    {
        public int MovieId { get; set; }
        public int CinemaId { get; set; }
        public int? MovieGenreId { get; set; }


        [Required]
        [Display(Name = "Is Bookable?")]
        public bool IsBookable { get; set; }

        [Required]
        [Display(Name = "Movie Name")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string MovieName { get; set; }

        [Required]
        [Display(Name = "Movie Description")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 15)]
        public string MovieDescription { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public string ReleaseDate { get; set; }

        [Required]
        [Display(Name = "Movie Length (minutes)")]
        public string MovieLength { get; set; }

        [Required]
        [Display(Name = "Price for Adults (in euros)")]
        public string PriceForAdults { get; set; }

        [Required]
        [Display(Name = "Price for Childrens (in euros)")]
        public string PriceForChildrens { get; set; }

        [Required]
        public string ShowTimeIds { get; set; }

        public string Rating { get; set; }

        [Required]
        public int LanguageId { get; set; }

        [Required]
        public int? Image { get; set; }

        public string CreatedByUserId { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string CreatedOnDate { get; set; }
        public string LastModifiedOnDate { get; set; }
        public bool IsDeleted { get; set; }

        public TblCinema Cinema { get; set; }
        public AspNetUsers CreatedByUser { get; set; }
        public Images ImageNavigation { get; set; }
        public TblLanguage Language { get; set; }
        public AspNetUsers LastModifiedByUser { get; set; }
        public TblMovieGenre MovieGenre { get; set; }
        public ICollection<TblCustomerComments> TblCustomerComments { get; set; }
        public ICollection<TblReservations> TblReservations { get; set; }
        public ICollection<TblShowTime> TblShowTime { get; set; }
        public ICollection<TblTicket> TblTicket { get; set; }
    }
}
