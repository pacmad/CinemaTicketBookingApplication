using System;
using System.Linq;
using CinemaTicketBooking.Entities;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketBookingTest
{
    public class DbFixture
    {
        public DbFixture()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection
                .AddDbContext<CinemaTicketBookingContext>(options => options.UseSqlServer("Server=RINORS-G5;Database=CinemaTicketBooking;User Id=sa; Password=P@ssw0rd;"),
                    ServiceLifetime.Transient);

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }
    }

    public class UnitTest1 : IClassFixture<DbFixture>
    {
        private ServiceProvider _serviceProvider;

        public UnitTest1(DbFixture fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
        }

        [Fact]
        public void AddCinemaTest()
        {
            using (var context = _serviceProvider.GetService<CinemaTicketBookingContext>())
            {
                TblCinema cinema = new TblCinema()
                {
                    CinemaName = "Test cinema",
                    CinemaDescription = "Test cinema description",
                    AdminUserId = "fa80f392-58b4-4415-ba40-c118c0651801",
                    CinemaProfilePicture = "test url",
                    AdressId = 2,
                    SeatsRows = 10,
                    SeatColumns = 30,
                    CreatedByUserId = "fa80f392-58b4-4415-ba40-c118c0651801",
                    LastModifiedByUserId = "fa80f392-58b4-4415-ba40-c118c0651801",
                    CreatedOnDate = DateTime.Now.ToString("dd/MM/yyyy"),
                    LastModifiedOnDate = DateTime.Now.ToString("dd/MM/yyyy"),
                    IsDeleted = false
                };

                context.TblCinema.Add(cinema);

                var added = context.SaveChanges() > 0;

                Assert.True(added);
            }
        }

        [Fact]
        public void GetCinemaTest()
        {
            using (var context = _serviceProvider.GetService<CinemaTicketBookingContext>())
            {
                var cinemaToGet = context.TblCinema.FirstOrDefault(r => r.CinemaName == "Test cinema");

                Assert.True(cinemaToGet != null);
            }
        }

        [Fact]
        public void EditCinemaTest()
        {
            using (var context = _serviceProvider.GetService<CinemaTicketBookingContext>())
            {
                var cinemaToGet = context.TblCinema.FirstOrDefault(r => r.CinemaName == "Test cinema");

                if (cinemaToGet != null)
                {
                    cinemaToGet.CinemaDescription = "Description has changed";
                    context.TblCinema.Update(cinemaToGet);
                    context.Entry(cinemaToGet).State = EntityState.Modified;
                }

                Assert.True(context.SaveChanges() > 0);
            }
        }


        [Fact]
        public void DeleteCinemaTest()
        {
            using (var context = _serviceProvider.GetService<CinemaTicketBookingContext>())
            {
                var tblCinema = context.TblCinema.SingleOrDefault(r => r.CinemaName == "Test cinema");

                if (tblCinema != null)
                {
                    tblCinema.IsDeleted = true;
                    context.TblCinema.Update(tblCinema);
                    context.Entry(tblCinema).State = EntityState.Modified;
                    var deleted = context.SaveChanges() > 0;
                    Assert.True(deleted);
                }
            }
        }
    }
}
