using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using _3MeePOSapi.Models;
using _3MeePOSapi.Services;
using System.IO;
using System.Net.Http.Headers;


namespace _3MeePOSapi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }


        [HttpGet]
        public ActionResult<List<Order>> GetAllOrder() => _orderService.GetOrderAll();


        [HttpGet("{id}")]
        public ActionResult<Order> GetOrderById(string id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }

            return order;
        }


       [HttpGet("{userId}/{orderId}")]
        public ActionResult<Order> GetOrderByCustomerIdAndOrderId(string userId , string orderId)
        {
         
            var order = _orderService.GetOrderByCustomerIdAndOrderId(userId , orderId);
            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        [HttpGet("{userId}")]
        public ActionResult<Order> GetOrderByCustomerId(string userId) =>_orderService.GetOrderByCustomerId(userId);


        [HttpGet("{userId}/{status}")]
        public ActionResult<List<Order>> ListOrderByCustomerIdAndStatus(string userId , string status) =>_orderService.ListOrderByCustomerIdAndStatus(userId , status);
        
        [HttpGet("{userId}/{status}")]
        public ActionResult<Order> GetOrderByCustomerIdAndStatus(string userId , string status) =>_orderService.GetOrderByCustomerIdAndStatus(userId , status);


        [HttpGet("{userId}/{orderId}")]
        public bool CheckOrderStatus(string userId , string orderId)
        {
             var user = _orderService.GetOrderByCustomerId(userId);
             var order = _orderService.GetOrderByCustomerId(orderId);
            //  var id = _orderService.GetOrderById(id)
             if (user == null && order == null)
             {
                 return true;
             }else
             {
                 return false;
             }
        }


        [HttpGet("{status}")]
        public ActionResult<List<Order>> GetOrderByStatus(string status)
        {
            var order = _orderService.GetOrderByStatus(status);
            if (order == null){
                
                return NotFound();
            }

            return order;
        }



        [HttpPost]
        public Order CreateOrder([FromBody] Order order)
        {
            var data = _orderService.GetOrderAll();
            var count = data.Count();
            var id = "A00" + count.ToString();
            order.OrderId = id;
            _orderService.CreateOrder(order);
            return order;
        }



        [HttpPut("{id}")]
        public IActionResult EditOrder( string id ,[FromBody] Order order)
        {
            var orders = _orderService.GetOrderById(id);
            if (orders == null)
            {
                return NotFound();
            }
            order.OrderId = id;
            _orderService.UpdateOrder(id, order);
            return NoContent();
        }



        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if(file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))

                    {
                     file.CopyTo(stream);
                    }

                return Ok(new {dbPath});
                } else 
                {
                return BadRequest();
                }
            } catch (System.Exception)
            {
                return StatusCode(500, $"Internal Server Error");
            }
        }
    }
    
}