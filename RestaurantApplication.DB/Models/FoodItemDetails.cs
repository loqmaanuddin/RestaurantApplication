using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApplication.DB.Models
{
    public class FoodItemDetaills
    {
        [Key]
        public int FoodDetailId
        {
            get;
            set;
        }
        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity
        {
            get;
            set;
        }
        [Required(ErrorMessage = "Quantity is required")]
        public int ItemCode
        {
            get;
            set;
        }
        public int OrderId
        {
            get;
            set;
        }
        [Required(ErrorMessage = "Quantity is required")]
        public int MenuCode
        {
            get;
            set;
        }

    }
}
