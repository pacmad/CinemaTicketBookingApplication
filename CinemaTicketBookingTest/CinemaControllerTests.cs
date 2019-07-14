using CinemaTicketBooking.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemaTicketBooking.Entities;
using CinemaTicketBooking.Models;
using CinemaTicketBooking.Models.SuperAdminViewModels;
using CinemaTicketBooking.Repository;
using Microsoft.AspNetCore.Identity;
using CinemaTicketBooking.Services;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CinemaTicketBooking.Extensions;

namespace CinemaTicketBookingTest
{
    public class CinemaControllerTests
    {
        ICinemaService _service;
        CinemaTicketBookingContext _context;
        UserManager<ApplicationUser> _userManager;
        ILogger _logger;
        CinemasController _controller;
        IImageHandler _imageHandler;

        public CinemaControllerTests(CinemaTicketBookingContext context,
            UserManager<ApplicationUser> userManager,
            ILogger<AccountController> logger,
            ICinemaService cinemaService,
            IImageHandler imageHandler)
        {
            _service = new CinemaServiceFake();
            _userManager = userManager;
            _context = context;
            _logger = logger;
            _imageHandler = imageHandler;
            _controller = new CinemasController(context, userManager, logger, cinemaService, _imageHandler);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.View();

            // Assert
            Assert.IsType<OkObjectResult>(okResult.ViewName);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _controller.ViewData.DefaultIfEmpty() as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<CinemaViewModel>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }

        [Fact]
        public async Task GetById_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = await _controller.Details(1);

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult.ToString());
        }

        [Fact]
        public async Task GetById_ExistingGuidPassed_ReturnsOkResult()
        {
            // Arrange
            var id = 53;

            // Act
            var okResult = await _controller.DeleteConfirmed(id);

            // Assert
            Assert.IsType<OkObjectResult>(okResult.ToString());
        }
    }
}
