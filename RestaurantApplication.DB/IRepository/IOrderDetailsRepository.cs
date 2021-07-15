using RestaurantApplication.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApplication.DB.IRepository
{
    public interface IOrderDetailsRepository : IDisposable
    {
        OrderDetails ViewOrderDetailsById(int orderId);
        int AddOrder(OrderDetails orderDetails);
        int UpdateOrder(OrderDetails orderDetails);
        string CancelOrder(int orderId);
    }
}
