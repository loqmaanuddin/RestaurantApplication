using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestaurantApplication.DB.Models
{
    [Table("MenuDetails")]
    public class MenuDetail
    {

        public DateTime Date
        {
            get;
            set;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Menu Code is required")]
        public int MenuCode
        {
            get;
            set;
        }
        //foreign key

        public List<MenuItem> MenuItems { get; set; }
        public List<FoodItemDetaills> foodItemDetaills { get; set; }

    }

}