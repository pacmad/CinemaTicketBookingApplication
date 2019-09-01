using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaTicketBooking.Entities
{
    public partial class AspNetUsers
    {
        public AspNetUsers()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaims>();
            AspNetUserLogins = new HashSet<AspNetUserLogins>();
            AspNetUserRoles = new HashSet<AspNetUserRoles>();
            AspNetUserTokens = new HashSet<AspNetUserTokens>();
            TblAddressCreatedByUser = new HashSet<TblAddress>();
            TblAddressLastModifiedByUser = new HashSet<TblAddress>();
            TblCinemaAdminUser = new HashSet<TblCinema>();
            TblCinemaCreatedByUser = new HashSet<TblCinema>();
            TblCinemaLastModifiedByUser = new HashSet<TblCinema>();
            TblCitiesCreatedByUser = new HashSet<TblCities>();
            TblCitiesLastModifiedByUser = new HashSet<TblCities>();
            TblCountriesCreatedByUser = new HashSet<TblCountries>();
            TblCountriesLastModifiedByUser = new HashSet<TblCountries>();
            TblCustomerCommentsCreatedByUser = new HashSet<TblCustomerComments>();
            TblCustomerCommentsLastModifiedByUser = new HashSet<TblCustomerComments>();
            TblLanguageCreatedByUser = new HashSet<TblLanguage>();
            TblLanguageLastModifiedByUser = new HashSet<TblLanguage>();
            TblMovieCreatedByUser = new HashSet<TblMovie>();
            TblMovieGenreCreatedByUser = new HashSet<TblMovieGenre>();
            TblMovieGenreLastModifiedByUser = new HashSet<TblMovieGenre>();
            TblMovieLastModifiedByUser = new HashSet<TblMovie>();
            TblPaymentTypeCreatedByUser = new HashSet<TblPaymentType>();
            TblPaymentTypeLastModifiedByUser = new HashSet<TblPaymentType>();
            TblPromotionCreatedByUser = new HashSet<TblPromotion>();
            TblPromotionLastModifiedByUser = new HashSet<TblPromotion>();
            TblReservationStatusCreatedByUser = new HashSet<TblReservationStatus>();
            TblReservationStatusLastModifiedByUser = new HashSet<TblReservationStatus>();
            TblReservationsCreatedByUser = new HashSet<TblReservations>();
            TblReservationsLastModifiedByUser = new HashSet<TblReservations>();
            TblReservationsReservedByCustomer = new HashSet<TblReservations>();
            TblShowTimeCreatedByUser = new HashSet<TblShowTime>();
            TblShowTimeLastModifiedByUser = new HashSet<TblShowTime>();
            TblTicketCreatedByUser = new HashSet<TblTicket>();
            TblTicketCustomer = new HashSet<TblTicket>();
            TblTicketLastModifiedByUser = new HashSet<TblTicket>();
        }

        public string Id { get; set; }
        public int AccessFailedCount { get; set; }
        public string ConcurrencyStamp { get; set; }

        [Required]
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public string NormalizedEmail { get; set; }
        public string NormalizedUserName { get; set; }
        public string PasswordHash { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string SecurityStamp { get; set; }
        public bool TwoFactorEnabled { get; set; }

        [Required]
        public string UserName { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }
        public ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }
        public ICollection<AspNetUserRoles> AspNetUserRoles { get; set; }
        public ICollection<AspNetUserTokens> AspNetUserTokens { get; set; }
        public ICollection<TblAddress> TblAddressCreatedByUser { get; set; }
        public ICollection<TblAddress> TblAddressLastModifiedByUser { get; set; }
        public ICollection<TblCinema> TblCinemaAdminUser { get; set; }
        public ICollection<TblCinema> TblCinemaCreatedByUser { get; set; }
        public ICollection<TblCinema> TblCinemaLastModifiedByUser { get; set; }
        public ICollection<TblCities> TblCitiesCreatedByUser { get; set; }
        public ICollection<TblCities> TblCitiesLastModifiedByUser { get; set; }
        public ICollection<TblCountries> TblCountriesCreatedByUser { get; set; }
        public ICollection<TblCountries> TblCountriesLastModifiedByUser { get; set; }
        public ICollection<TblCustomerComments> TblCustomerCommentsCreatedByUser { get; set; }
        public ICollection<TblCustomerComments> TblCustomerCommentsLastModifiedByUser { get; set; }
        public ICollection<TblLanguage> TblLanguageCreatedByUser { get; set; }
        public ICollection<TblLanguage> TblLanguageLastModifiedByUser { get; set; }
        public ICollection<TblMovie> TblMovieCreatedByUser { get; set; }
        public ICollection<TblMovieGenre> TblMovieGenreCreatedByUser { get; set; }
        public ICollection<TblMovieGenre> TblMovieGenreLastModifiedByUser { get; set; }
        public ICollection<TblMovie> TblMovieLastModifiedByUser { get; set; }
        public ICollection<TblPaymentType> TblPaymentTypeCreatedByUser { get; set; }
        public ICollection<TblPaymentType> TblPaymentTypeLastModifiedByUser { get; set; }
        public ICollection<TblPromotion> TblPromotionCreatedByUser { get; set; }
        public ICollection<TblPromotion> TblPromotionLastModifiedByUser { get; set; }
        public ICollection<TblReservationStatus> TblReservationStatusCreatedByUser { get; set; }
        public ICollection<TblReservationStatus> TblReservationStatusLastModifiedByUser { get; set; }
        public ICollection<TblReservations> TblReservationsCreatedByUser { get; set; }
        public ICollection<TblReservations> TblReservationsLastModifiedByUser { get; set; }
        public ICollection<TblReservations> TblReservationsReservedByCustomer { get; set; }
        public ICollection<TblShowTime> TblShowTimeCreatedByUser { get; set; }
        public ICollection<TblShowTime> TblShowTimeLastModifiedByUser { get; set; }
        public ICollection<TblTicket> TblTicketCreatedByUser { get; set; }
        public ICollection<TblTicket> TblTicketCustomer { get; set; }
        public ICollection<TblTicket> TblTicketLastModifiedByUser { get; set; }
    }
}
