using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestaurantApplication.DB.IRepository;
using RestaurantApplication.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Http;


namespace RestaurantApplication.Controllers
{
    [RoutePrefix("menu")]
    public class MenuController : ApiController
    {
        IMenuDetailRepository _munuRepo = null;
        MenuDetail menuResponse;
        public MenuController(IMenuDetailRepository menuRepo)
        {
            _munuRepo = menuRepo;
        }
        [HttpPost]
        [Route("addmenu")]
        public IHttpActionResult AddMenu(HttpRequestMessage request)
        {

            string json = request.Content.ReadAsStringAsync().Result;
            var arg = JsonConvert.DeserializeObject<MenuDetail>(json);
            menuResponse = new MenuDetail();
            int response = _munuRepo.AddMenu(arg);
            if(response > 0)
            {
                DateTime date = DateTime.Now.Date;
                menuResponse = _munuRepo.ViewMenuByMenuCode(response);
            }
            var actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, menuResponse));
            return actionResult;

        }

        [HttpGet]
        [Route("viewmenubydate")]
        public IHttpActionResult ViewMenuByDate(HttpRequestMessage request)
        {
            string json = request.Content.ReadAsStringAsync().Result;
            var arg = JsonConvert.DeserializeObject<DateTime?>(json);
            
            DateTime? requiredDate;
            if (arg != null)
            {
                requiredDate = arg;
            }
            else
            {
                requiredDate = DateTime.Now.Date;
            }
            var menuResponse = _munuRepo.ViewMenubyDate(requiredDate);
            var actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, menuResponse));
            return actionResult;

        }
        [HttpGet]
        [Route("viewmenubymenucode")]
        public IHttpActionResult ViewMenuByMenuCode(HttpRequestMessage request)
        {
            string json = request.Content.ReadAsStringAsync().Result;
            int menuCode = JsonConvert.DeserializeObject<int>(json);
            MenuDetail menuResponse = _munuRepo.ViewMenuByMenuCode(menuCode);
            var actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, menuResponse));
            return actionResult;
        }

        [HttpPost]
        [Route("updatemenu")]
        public IHttpActionResult UpdateMenu(HttpRequestMessage request)
        {
            string json = request.Content.ReadAsStringAsync().Result;
            var arg = JsonConvert.DeserializeObject<MenuDetail>(json);
            int response = _munuRepo.UpdateMenu(arg);
            if (response > 0)
            {
                DateTime date = DateTime.Now.Date;
                menuResponse = _munuRepo.ViewMenuByMenuCode(response);
            }
            var actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, menuResponse));
            return actionResult;

        }

    }
}