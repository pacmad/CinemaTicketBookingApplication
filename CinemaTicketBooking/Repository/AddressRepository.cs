using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemaTicketBooking.Entities;

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

        public bool EditAddress(TblAddress cinema)
        {
            throw new NotImplementedException();
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
