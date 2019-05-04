using System;
using System.Collections.Generic;

namespace CinemaTicketBooking.Entities
{
    public partial class TblFeedback
    {
        public int FeedbackId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
