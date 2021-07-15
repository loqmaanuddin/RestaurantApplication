using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestaurantApplication.DB.Models
{
    [Table("BillDetails")]
    public class BillDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int InvoiceNumber
        {
            get;
            set;
        }

        public decimal DiscountAmount
        {
            get;
            set;
        }
        public decimal TipAmount
        {
            get;
            set;
        }
        public int BillAmount
        {
            get;
            set;
        }
        public int OrderId
        {
            get;
            set;
        }
        public OrderDetails OrderDetails
        {
            get;
            set;
        }
    }
}