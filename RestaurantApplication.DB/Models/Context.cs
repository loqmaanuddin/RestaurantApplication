using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApplication.DB.Models
{
    public class Context : DbContext
    {
        public Context() : base("name = DefaultConnection") { }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<MenuDetail> MenuDetail { get; set; }
        public DbSet<MenuItem> MenuItem { get; set; }
        public DbSet<FoodItemDetaills> FoodItemDetaills { get; set; }
        public DbSet<BillDetails> BillDetails { get; set; }
    }
}
