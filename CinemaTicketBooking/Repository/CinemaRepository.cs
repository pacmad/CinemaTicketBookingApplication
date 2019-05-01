using CinemaTicketBooking.Entities;
using CinemaTicketBooking.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTicketBooking.Repository
{
    public class CinemaRepository : ICinemaRepostiory
    {
        private readonly CinemaTicketBookingContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CinemaRepository(CinemaTicketBookingContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public bool AddCinema(TblCinema cinema)
        {
            _context.TblCinema.Add(cinema);
            return _context.SaveChanges() > 0;
        }

        public bool DeleteCinema(int id)
        {
            var tblCinema = _context.TblCinema.SingleOrDefault(m => m.CinemaId == id);
            tblCinema.IsDeleted = true;
            _context.TblCinema.Update(tblCinema);
            _context.Entry(tblCinema).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        }

        public bool EditCinema(TblCinema cinema)
        {
            _context.TblCinema.Update(cinema);
            _context.Entry(cinema).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        }

        public List<TblCinema> GetAllCinemas()
        {
            var cinemaTicketBookingContext = _context.TblCinema.Include(t => t.AdminUser).Include(t => t.Adress).Include(t => t.CreatedByUser).Include(t => t.LastModifiedByUser).Where(r => r.IsDeleted == false).ToList();
            return cinemaTicketBookingContext;
        }

        public TblCinema GetCinemaById(int id)
        {
            var tblCinema = _context.TblCinema
                .Include(t => t.AdminUser)
                .Include(t => t.Adress)
                .Include(t => t.CreatedByUser)
                .Include(t => t.LastModifiedByUser)
                .Where(r => r.IsDeleted == false && r.CinemaId == id).SingleOrDefault();

            return tblCinema;
        }
    }
}
