using Newtonsoft.Json;
using RestaurantApplication.DB.IRepository;
using RestaurantApplication.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestaurantApplication.Controllers
{
    [RoutePrefix("order")]
    public class OrderController : ApiController
    {
        IOrderDetailsRepository _orderRepo = null;
        OrderDetails orderDetails;
        public OrderController(IOrderDetailsRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }

        [HttpGet]
        [Route("vieworder")]
        public IHttpActionResult ViewOrder(HttpRequestMessage request)
        {
            string json = request.Content.ReadAsStringAsync().Result;
            int orderId = JsonConvert.DeserializeObject<int>(json);
            orderDetails = new OrderDetails();
            orderDetails = _orderRepo.ViewOrderDetailsById(orderId);
            var actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, orderDetails));
            return actionResult;

        }
        [HttpPost]
        [Route("addorder")]
        public IHttpActionResult AddOrder(HttpRequestMessage request)
        {
            string json = request.Content.ReadAsStringAsync().Result;
            var newOrder = JsonConvert.DeserializeObject<OrderDetails>(json);
            orderDetails = new OrderDetails();
            int orderId = _orderRepo.AddOrder(newOrder);
            if (orderId > 0)
            {
                orderDetails = _orderRepo.ViewOrderDetailsById(orderId);
                
            }
            var actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, orderDetails));
            return actionResult;

        }
        [HttpPost]
        [Route("updateorder")]
        public IHttpActionResult UpdateOrder(HttpRequestMessage request)
        {
            string json = request.Content.ReadAsStringAsync().Result;
            var newOrder = JsonConvert.DeserializeObject<OrderDetails>(json);
            orderDetails = new OrderDetails();
            int response = _orderRepo.UpdateOrder(newOrder);
            if (response > 0)
            {
                orderDetails = _orderRepo.ViewOrderDetailsById(response);

            }
            var actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, orderDetails));
            return actionResult;

        }
        [HttpPost]
        [Route("cancelorder")]
        public IHttpActionResult CancelOrder(HttpRequestMessage request)
        {
            string json = request.Content.ReadAsStringAsync().Result;
            var orderId = JsonConvert.DeserializeObject<int>(json);
            var Response = _orderRepo.CancelOrder(orderId);
            var actionResult = ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, Response));
            return actionResult;
        }


    }
}
