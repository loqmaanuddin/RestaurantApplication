using RestaurantApplication.DB.IRepository;
using RestaurantApplication.DB.Models;
using RestaurantApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestaurantApplication.DB.Common;

namespace RestaurantApplication.BLL
{
    public class BillGenerator
    {
        IOrderDetailsRepository orderRepo = null;
        IMenuDetailRepository menuRepo = null;
        BillDetails finalbill = null;
        public BillGenerator(IOrderDetailsRepository orderDetailsRepository, IMenuDetailRepository menuDetailRepository)
        {
            orderRepo = orderDetailsRepository;
            menuRepo = menuDetailRepository;
        }
        public BillDetails GenerateBill(BillRequest billRequest)
        {
            int amount = 0;
            int discount = 0;
            int tip = 0;
            int totalBillAmount = 0;
            OrderDetails orderDetails = new OrderDetails();
            orderDetails.foodItemDetaills = new List<FoodItemDetaills>();
            orderDetails = orderRepo.ViewOrderDetailsById(billRequest.OrderId);
            if (orderDetails != null && orderDetails.foodItemDetaills.Count > 0)
            {
                foreach (var item in orderDetails.foodItemDetaills)
                {
                    int price = GetItemPrice(item.ItemCode, item.MenuCode);
                    amount = item.Quantity * price + amount;
                }

            }
            if (amount > 0 && (billRequest.DiscountAmount > 0 || billRequest.DiscountInPercentage > 0))
            {
                int percentageDiscount = (amount * billRequest.DiscountInPercentage) / (100);
                discount = billRequest.DiscountAmount + percentageDiscount;
            }
            if (billRequest.TipAmount > 0 || billRequest.TipInPercentage > 0)
            {
                tip = (amount > 0) ? ((amount * billRequest.TipInPercentage) / (100)) : 0;
                tip = tip + billRequest.TipAmount;
            }
            totalBillAmount = (amount + tip - discount);
            int invoiceNumber = DB.Common.Utility.GetRandomNumber(5);
            finalbill = new BillDetails
            {
                InvoiceNumber = invoiceNumber,
                DiscountAmount = discount,
                TipAmount = tip,
                BillAmount = totalBillAmount,
                OrderId = billRequest.OrderId
            };
            return finalbill;
        }
        public int GetItemPrice(int itemCode, int menuCode)
        {
            var date = DateTime.Now.Date;
            int price = 0;
            MenuDetail menuDetails = menuRepo.ViewMenuByMenuCode(menuCode);
            if (menuDetails != null)
            {
                price = menuDetails.MenuItems.Where(x => x.ItemCode == itemCode).FirstOrDefault().Price;
            }
            return price;

        }

    }
}