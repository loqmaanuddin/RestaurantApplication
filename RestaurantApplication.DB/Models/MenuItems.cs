using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApplication.DB.Models
{
    [Table("MenuItems")]
    public class MenuItem
    {
        [Key]
        public int Id
        {
            get;
            set;
        }
        [Required(ErrorMessage = "Item Code is required")]
        public int ItemCode
        {
            get;
            set;
        }
        public string ItemName
        {
            get;
            set;
        }
        [Required(ErrorMessage = "Price is required")]
        public int Price
        {
            get;
            set;
        }
        public int MenuCode
        {
            get;
            set;
        }

    }
}
