using CinemaTicketBooking.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTicketBooking.Models.SuperAdminViewModels
{
    public class CinemaViewModel
    {
        public int CinemaId { get; set; }
        public string AdminUserId { get; set; }

        [Required]
        [Display(Name = "Cinema Name")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string CinemaName { get; set; }

        [Required]
        [Display(Name = "Cinema Description")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 20)]
        public string CinemaDescription { get; set; }

        [Display(Name = "Cinema Profile Picture")]
        public string CinemaProfilePicture { get; set; }
        public int AdressId { get; set; }
        public string CreatedByUserId { get; set; }
        public string LastModifiedByUserId { get; set; }

        [Display(Name = "Created on date")]
        public string CreatedOnDate { get; set; }

        [Display(Name = "Last modified on date")]
        public string LastModifiedOnDate { get; set; }
        public bool IsDeleted { get; set; }

        public int CityId { get; set; }
        public int CountryId { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string StreetName { get; set; }

        [Display(Name = "Admin user")]
        public AspNetUsers AdminUser { get; set; }

        public TblAddress Adress { get; set; }
        public AspNetUsers CreatedByUser { get; set; }
        public AspNetUsers LastModifiedByUser { get; set; }
        public ICollection<TblCustomerComments> TblCustomerComments { get; set; }
        public ICollection<TblMovie> TblMovie { get; set; }
        public ICollection<TblReservations> TblReservations { get; set; }
        public ICollection<TblSeat> TblSeat { get; set; }
        public ICollection<TblShowTime> TblShowTime { get; set; }
        public ICollection<TblTicket> TblTicket { get; set; }



    }
}
