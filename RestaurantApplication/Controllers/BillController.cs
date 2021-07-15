using Newtonsoft.Json;
using RestaurantApplication.BLL;
using RestaurantApplication.DB.IRepository;
using RestaurantApplication.DB.Models;
using RestaurantApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestaurantApplication.Controllers
{
    [RoutePrefix("bill")]
    public class BillController : ApiController
    {
        IBillDetailsRepository _billRepo = null;
        readonly IOrderDetailsRepository _orderRepo = null;
        readonly IMenuDetailRepository _menuRepo = null;

        public BillController(IBillDetailsRepository billRepo, IOrderDetailsRepository orderRepo, IMenuDetailRepository menuRepo)
        {
            _billRepo = billRepo;
            _orderRepo = orderRepo;
            _menuRepo = menuRepo;
        }

        [HttpGet]
        [Route("viewbill")]
        public IHttpActionResult ViewBill(HttpRequestMessage request)
        {
            string json = request.Content.ReadAsStringAsync().Result;
            int invoiceNumber = JsonConvert.DeserializeObject<int>(json);
            var Response = _billRepo.ViewBill(invoiceNumber);
            var actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, Response));
            return actionResult;
        }

        [HttpPost]
        [Route("generatebill")]
        public IHttpActionResult GenerateBill(HttpRequestMessage request)
        {
            string json = request.Content.ReadAsStringAsync().Result;
            var billRequest = JsonConvert.DeserializeObject<BillRequest>(json);
            BillGenerator billGenerator = new BillGenerator(_orderRepo, _menuRepo);
            BillDetails billDetails = new BillDetails();
            billDetails = billGenerator.GenerateBill(billRequest);
            int response = _billRepo.AddBill(billDetails);
            var actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, billDetails));
            return actionResult;

        }
    }
}
