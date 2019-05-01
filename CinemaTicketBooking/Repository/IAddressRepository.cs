using CinemaTicketBooking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTicketBooking.Repository
{
    public interface IAddressRepository
    {
        TblAddress GetAddressById(int id);
        List<TblAddress> GetAllAddress();
        bool AddAddress(TblAddress cinema);
        bool EditAddress(TblAddress cinema);
        bool DeleteAddress(int id);
    }
}
