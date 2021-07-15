using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantApplication.DB.IRepository;
using RestaurantApplication.DB.Models;

namespace RestaurantApplication.DB.Repository
{
    public class BillDetailsRepository : IBillDetailsRepository
    {
        private DbSet<BillDetails> entities;
        private readonly Context dbContext;
        public BillDetailsRepository(Context db)
        {
            dbContext = db;
            entities = db.BillDetails;
        }

        public BillDetails ViewBill(int invoiceNunmber)
        {
            return entities.Find(invoiceNunmber);
        }

        public int AddBill(BillDetails billDetails)
        {
            int result = -1;

            if (billDetails != null)
            {
                dbContext.BillDetails.Add(billDetails);
                dbContext.SaveChanges();
                result = billDetails.InvoiceNumber;
            }
            return result;
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
