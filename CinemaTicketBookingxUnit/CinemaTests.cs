using Castle.Core.Logging;
using CinemaTicketBooking.Controllers;
using CinemaTicketBooking.Entities;
using CinemaTicketBooking.Extensions;
using CinemaTicketBooking.Models;
using CinemaTicketBooking.Models.SuperAdminViewModels;
using CinemaTicketBooking.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Xunit;
using Moq;

namespace CinemaTicketBookingxUnit
{
    public class CinemaTests
    {
        private readonly ICinemaService _cinemaService;

        public CinemaTests(ICinemaService cinemaService)
        {
            _cinemaService = cinemaService;
        }

        [Fact]
        public void GetAllCinemas()
        {
            CinemaViewModel cinema = new CinemaViewModel()
            {
                CinemaName = "My testing cinema",
                CinemaDescription = "My testing cinema description",
                CinemaProfilePicture = "Test picture url",
                AdminUserId = "d3956f65-2089-470c-b0d9-a15832c8bdab",
                AdressId = 2,
                SeatsRows = 10,
                SeatColumns = 20,
                CreatedByUserId = "d3956f65-2089-470c-b0d9-a15832c8bdab",
                LastModifiedByUserId = "d3956f65-2089-470c-b0d9-a15832c8bdab",
                CreatedOnDate = DateTime.Now.ToString("dd/MM/yyyy"),
                LastModifiedOnDate = DateTime.Now.ToString("dd/MM/yyyy"),
                IsDeleted = false
            };

            Assert.True(_cinemaService.AddCinema(cinema));
        }
    }
}
