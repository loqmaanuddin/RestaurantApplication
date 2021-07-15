using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestaurantApplication.Controllers;
using RestaurantApplication.DB.Models;
using RestaurantApplication.DB.Repository;

namespace RestaurantApplication.Tests
{
    [TestClass]
    public class MenuControllerTest
    {
        public MenuController menuController = null;
        public MenuDetailRepository menuRepo = null;
        public Context dbContext = null;
        int menuCode = 0;

        [TestInitialize]
        public void TestInitialize()
        {
            dbContext = new Context();
            menuRepo = new MenuDetailRepository(dbContext);
            menuController = new MenuController(menuRepo);
            Database.SetInitializer<Context>(new DropCreateDatabaseIfModelChanges<Context>());
            menuCode = DB.Common.Utility.GetRandomNumber(3);
            menuController.Configuration = new HttpConfiguration();
            HttpContext.Current = new HttpContext(new HttpRequest(null, "http://tempuri.org", null), new HttpResponse(null));
        }
        [TestMethod]
        public void AddMenuTest()
        {
            
            MenuItem menuItem1 = new MenuItem
            {
                ItemCode = 103,
                ItemName = "Manchurya",
                Price = 100
            };
            MenuItem menuItem2 = new MenuItem
            {
                ItemCode = 104,
                ItemName = "Kabab",
                Price = 150
            };
            MenuDetail request = new MenuDetail
            {
                Date = DateTime.Now,
                MenuCode = menuCode,
                MenuItems = new List<MenuItem>
                {
                    menuItem1,menuItem2

                }

            };
            var requestJson = JsonConvert.SerializeObject(request);
            menuController.Request = new HttpRequestMessage();
            menuController.Request.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");
            var result = menuController.AddMenu(menuController.Request);
            var resultData = result as ResponseMessageResult;
            var data = resultData.Response.Content.ReadAsAsync<MenuDetail>().Result;
            Assert.IsNotNull(data);
        }

        [TestMethod]
        public void ViewMenuByDateTest()
        {
            DateTime request = DateTime.Now.Date;
            var requestJson = JsonConvert.SerializeObject(request);
            menuController.Request = new HttpRequestMessage();
            menuController.Request.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");
            var result = menuController.ViewMenuByDate(menuController.Request);
            var resultData = result as ResponseMessageResult;
            var data = resultData.Response.Content.ReadAsAsync<List<MenuDetail>>().Result;
            Assert.IsNotNull(data);
        }
        [TestMethod]
        public void ViewMenuByMenuCodeTest()
        {
            var requestJson = JsonConvert.SerializeObject(menuCode);
            menuController.Request = new HttpRequestMessage();
            menuController.Request.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");
            var result = menuController.ViewMenuByMenuCode(menuController.Request);
            var resultData = result as ResponseMessageResult;
            var data = resultData.Response.Content.ReadAsAsync<MenuDetail>().Result;
            Assert.IsNotNull(data);
        }
        [TestMethod]
        public void UpdateMenuTest()
        {
            MenuItem menuItem1 = new MenuItem
            {
                ItemCode = 166,
                ItemName = "Biryani",
                Price = 200
            };
            MenuItem menuItem2 = new MenuItem
            {
                ItemCode = 164,
                ItemName = "Noodles",
                Price = 150
            };
            MenuDetail request = new MenuDetail
            {
                Date = DateTime.Now,
                MenuCode = menuCode,
                MenuItems = new List<MenuItem>
                {
                    menuItem1,menuItem2

                }

            };
           
            var requestJson = JsonConvert.SerializeObject(request);
            menuController.Request = new HttpRequestMessage();
            menuController.Request.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");
            var result = menuController.UpdateMenu(menuController.Request);
            var resultData = result as ResponseMessageResult;
            var data = resultData.Response.Content.ReadAsAsync<MenuDetail>().Result;
            Assert.IsNotNull(data);
        }
    }
}
