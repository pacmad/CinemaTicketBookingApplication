//using CinemaTicketBooking.Controllers;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using CinemaTicketBooking.Entities;
//using CinemaTicketBooking.Models;
//using CinemaTicketBooking.Models.SuperAdminViewModels;
//using CinemaTicketBooking.Repository;
//using Microsoft.AspNetCore.Identity;
//using CinemaTicketBooking.Services;
//using Xunit;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;

//namespace CinemaTicketBookingTest
//{
//    public class CinemaControllerTests
//    {
//        ICinemaService _service;
//        CinemaTicketBookingContext _context;
//        UserManager<ApplicationUser> _userManager;
//        ILogger _logger;
//        CinemasController _controller;

//        public CinemaControllerTests(CinemaTicketBookingContext context,
//            UserManager<ApplicationUser> userManager,
//            ILogger<AccountController> logger,
//            ICinemaService cinemaService)
//        {
//            _service = new CinemaServiceFake();
//            _controller = new CinemasController(context, userManager, logger, cinemaService);
//            _userManager = userManager;
//            _context = context;
//            _logger = logger;
//        }




//        [Fact]
//        public void Get_WhenCalled_ReturnsOkResult()
//        {
//            // Act
//            var okResult = _controller.View();

//            // Assert
//            Assert.IsType<OkObjectResult>(okResult.ViewName);
//        }

//        [Fact]
//        public void Get_WhenCalled_ReturnsAllItems()
//        {
//            // Act
//            var okResult = _controller.ViewData.DefaultIfEmpty() as OkObjectResult;

//            // Assert
//            var items = Assert.IsType<List<CinemaViewModel>>(okResult.Value);
//            Assert.Equal(3, items.Count);
//        }

//        [Fact]
//        public void GetById_UnknownGuidPassed_ReturnsNotFoundResult()
//        {
//            // Act
//            var notFoundResult = _controller.Get(Guid.NewGuid());

//            // Assert
//            Assert.IsType<NotFoundResult>(notFoundResult.Result);
//        }

//        [Fact]
//        public void GetById_ExistingGuidPassed_ReturnsOkResult()
//        {
//            // Arrange
//            var testGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

//            // Act
//            var okResult = _controller.Get(testGuid);

//            // Assert
//            Assert.IsType<OkObjectResult>(okResult.Result);
//        }

//        [Fact]
//        public void GetById_ExistingGuidPassed_ReturnsRightItem()
//        {
//            // Arrange
//            var testGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

//            // Act
//            var okResult = _controller.ViewBag[testGuid] as OkObjectResult;

//            // Assert
//            Assert.IsType<CinemaViewModel>(okResult.Value);
//            Assert.Equal(testGuid, (okResult.Value as CinemaViewModel).CinemaId);
//        }
//    }
//}
