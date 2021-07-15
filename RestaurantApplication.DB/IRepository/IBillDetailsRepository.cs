using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantApplication.DB.Models;

namespace RestaurantApplication.DB.IRepository
{
    public interface IBillDetailsRepository : IDisposable
    {
        int AddBill(BillDetails billDetails);
        BillDetails ViewBill(int invoiceNunmber);
    }
}
