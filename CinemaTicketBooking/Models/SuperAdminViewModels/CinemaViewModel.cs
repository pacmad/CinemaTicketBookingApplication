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
        public string CinemaName { get; set; }
        public string CinemaDescription { get; set; }
        public string CinemaProfilePicture { get; set; }
        public int AdressId { get; set; }
        public string CreatedByUserId { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string CreatedOnDate { get; set; }
        public string LastModifiedOnDate { get; set; }
        public bool IsDeleted { get; set; }

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
