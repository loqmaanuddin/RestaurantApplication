using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantApplication.DB.Models;

namespace RestaurantApplication.DB.IRepository
{
    public interface IMenuDetailRepository : IDisposable
    {
        MenuDetail ViewMenuByMenuCode(int menuCode);
        List<MenuDetail> ViewMenubyDate(DateTime? date);
        int AddMenu(MenuDetail menu);
        int UpdateMenu(MenuDetail menu);

    }
}
