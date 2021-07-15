using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestaurantApplication.DB.Models
{

    [Table("OrderDetails")]
    public class OrderDetails
    {
        public string CustomerName
        {
            get;
            set;
        }

        [Display(Name = "Table Number")]
        [Required(ErrorMessage = "Table Number is required")]
        public int TableNumber
        {
            get;
            set;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderId
        {
            get;
            set;
        }
        public List<FoodItemDetaills> foodItemDetaills { get; set; }
    }
}