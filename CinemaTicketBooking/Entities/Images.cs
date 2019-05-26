using System;
using System.Collections.Generic;

namespace CinemaTicketBooking.Entities
{
    public partial class Images
    {
        public Images()
        {
            TblMovie = new HashSet<TblMovie>();
        }

        public int ImageId { get; set; }
        public byte[] Name { get; set; }
        public byte[] Data { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string ContentType { get; set; }

        public ICollection<TblMovie> TblMovie { get; set; }
    }
}
