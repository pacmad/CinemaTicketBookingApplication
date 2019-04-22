using System;
using System.Collections.Generic;

namespace CinemaTicketBooking.Entities
{
    public partial class TblPromotion
    {
        public int PromotionId { get; set; }
        public string PromotionDescription { get; set; }
        public decimal PromotionDiscount { get; set; }
        public string PromotionStartDate { get; set; }
        public string PromotionEndDate { get; set; }
        public string CreatedByUserId { get; set; }
        public string LastModifiedByUserId { get; set; }
        public string CreatedOnDate { get; set; }
        public string LastModifiedOnDate { get; set; }
        public bool IsDeleted { get; set; }

        public AspNetUsers CreatedByUser { get; set; }
        public AspNetUsers LastModifiedByUser { get; set; }
    }
}
