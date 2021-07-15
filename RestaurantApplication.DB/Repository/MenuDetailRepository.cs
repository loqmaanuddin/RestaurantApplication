using RestaurantApplication.DB.IRepository;
using RestaurantApplication.DB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApplication.DB.Repository
{
    public class MenuDetailRepository : IMenuDetailRepository
    {
        private readonly DbSet<MenuDetail> entities;
        private readonly Context dbContext;
        public MenuDetailRepository(Context db)
        {
            dbContext = db;
            entities = db.MenuDetail;
        }
        public int UpdateMenu(MenuDetail menu)
        {
            int result = -1;
            if(menu != null)
            { 
            menu.Date = DateTime.Now.Date;
            MenuDetail menuDetails = dbContext.MenuDetail.Where(x =>x.MenuCode == menu.MenuCode).Include(x=>x.MenuItems).FirstOrDefault();
                //menuDetails.MenuItems = new List<MenuItem>();
                //menuDetails.MenuItems.AddRange(menu.MenuItems);

           // menuDetails.Add(menu);
           dbContext.MenuDetail.Remove(menuDetails);
           dbContext.MenuDetail.Add(menu);
           // dbContext.MenuDetail.Attach(menuDetails);
            dbContext.SaveChanges();
            result = menu.MenuCode;
            }
            return result;
        }

        public MenuDetail ViewMenuByMenuCode(int menuCode)
        {
            MenuDetail menuDetail = new MenuDetail();
            menuDetail = dbContext.MenuDetail.Where(x => x.MenuCode == menuCode).Select(x => x).Include(x => x.MenuItems).FirstOrDefault();
            if (menuDetail == null)
            {
                menuDetail = dbContext.MenuDetail.Select(x => x).Include(x => x.MenuItems).FirstOrDefault();
            }
            return menuDetail;
        }
        public List<MenuDetail> ViewMenubyDate(DateTime? date)
        {
            List<MenuDetail> menuList = new List<MenuDetail>();
            try
            {
                menuList = dbContext.MenuDetail.Where(x => x.Date == date).Include(x => x.MenuItems).ToList();
                if (menuList.Count() <= 0)
                {
                    dbContext.MenuDetail.Select(x => x).Include(x => x.MenuItems).FirstOrDefault();
                }

            }
            catch (Exception)
            {
                return new List<MenuDetail>();
            }
            return menuList;
        }

        public int AddMenu(MenuDetail menu)
        {
            int result = -1;

            if (menu != null)
            {
                menu.Date = DateTime.Now.Date;
                dbContext.MenuDetail.Add(menu);
                dbContext.SaveChanges();
                result = menu.MenuCode;
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
