using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantApplication.Models
{
    public class BillRequest
    {
        public int DiscountInPercentage { get; set; }
        public int TipInPercentage { get; set; }
        public int DiscountAmount { get; set; }
        public int TipAmount { get; set; }
        public int OrderId { get; set; }
    }
}