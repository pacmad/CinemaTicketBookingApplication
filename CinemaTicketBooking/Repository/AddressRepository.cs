using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemaTicketBooking.Entities;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketBooking.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly CinemaTicketBookingContext _context;

        public AddressRepository(CinemaTicketBookingContext context)
        {
            _context = context;
        }

        public bool AddAddress(TblAddress address)
        {
            _context.TblAddress.Add(address);
            return _context.SaveChanges() > 0;
        }

        public bool DeleteAddress(int id)
        {
            throw new NotImplementedException();
        }

        public bool EditAddress(TblAddress address)
        {
            _context.TblAddress.Update(address);
            _context.Entry(address).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        }

        public TblAddress GetAddressById(int id)
        {
            throw new NotImplementedException();
        }

        public List<TblAddress> GetAllAddress()
        {
            throw new NotImplementedException();
        }
    }
}
