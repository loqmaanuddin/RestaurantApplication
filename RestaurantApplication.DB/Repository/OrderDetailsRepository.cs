using RestaurantApplication.DB.Common;
using RestaurantApplication.DB.IRepository;
using RestaurantApplication.DB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApplication.DB.Repository
{
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        private DbSet<OrderDetails> entities;
        private readonly Context dbContext;
        public OrderDetailsRepository(Context db)
        {
            dbContext = db;
            entities = db.OrderDetails;
        }
        public int AddOrder(OrderDetails orderDetails)
        {
            int result = -1;

            if (orderDetails != null)
            {
                orderDetails.OrderId = Utility.GetRandomNumber(4);
                dbContext.OrderDetails.Add(orderDetails);
                dbContext.SaveChanges();
                result = orderDetails.OrderId;
            }
            return result;
        }

        public string CancelOrder(int orderId)
        {
            string msg = "success";
            try
            { 
                OrderDetails orderDetails = entities.Find(orderId);
                dbContext.OrderDetails.Remove(orderDetails);
                dbContext.SaveChanges();
            }
            catch 
            {
                msg = "Failed";
            }
            return "order cancel "+ msg;
        } 

        public int UpdateOrder(OrderDetails orderDetails)
        {
            int result = -1;

            if (orderDetails.foodItemDetaills.Count > 0)
            {
                OrderDetails previousOrder = dbContext.OrderDetails.Where(x => x.OrderId == orderDetails.OrderId).Select(x => x).Include(x => x.foodItemDetaills).FirstOrDefault();
                foreach (var newOrder in orderDetails.foodItemDetaills.ToList())
                {
                   
                    foreach(var pOrder in previousOrder.foodItemDetaills)
                    {
                        if(pOrder.ItemCode == newOrder.ItemCode)
                        {
                            pOrder.Quantity = newOrder.Quantity+ pOrder.Quantity;
                            orderDetails.foodItemDetaills.Remove(newOrder);
                        }

                    }
                    
                }
                previousOrder.foodItemDetaills.AddRange(orderDetails.foodItemDetaills);
                dbContext.OrderDetails.Attach(previousOrder);
                dbContext.SaveChanges();
                result = orderDetails.OrderId;
            }
            return result;
        }

        public OrderDetails ViewOrderDetailsById(int orderId)
        {
            return dbContext.OrderDetails.Where(x => x.OrderId == orderId).Select(x => x).Include(x => x.foodItemDetaills).FirstOrDefault();

        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
    }
}
