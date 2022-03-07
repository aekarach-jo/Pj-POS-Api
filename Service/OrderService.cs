using MongoDB.Driver;
using _3MeePOSapi.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace _3MeePOSapi.Services
{
    public class OrderService
    {
        private readonly IMongoCollection<Order> _order;

        public OrderService(DatabaseSetting settings)
        {
             var Client = new MongoClient(settings.ConnectionString);
            var database = Client.GetDatabase(settings.DatabaseName);
            var filter = Builders<Order>.Filter.Where(order => order.Status == "Open");

            _order = database.GetCollection<Order>(settings.OrderCollection); 
        }

        public List<Order> GetOrderAll() => _order.Find(order => true).ToList();

        public Order GetOrderById(string id) => _order.Find<Order>(order => order.OrderId == id).FirstOrDefault();

        public Order GetOrderByCustomerIdAndOrderId(string userId , string orderId) => _order.Find<Order>(order => order.UserId == userId && order.OrderId == orderId).FirstOrDefault();
       
        public Order GetOrderByCustomerId(string id) => _order.Find<Order>(order => order.UserId == id).FirstOrDefault();
        
        public List<Order> ListOrderByCustomerIdAndStatus(string id ,  string status) => _order.Find<Order>(order => order.UserId == id && order.Status == status).ToList();

       
        public Order GetOrderByCustomerIdAndStatus(string id , string status) => _order.Find<Order>(order => order.UserId == id && order.Status == status).FirstOrDefault();

        public List<Order> GetOrderByStatus(string status) => _order.Find(order => order.Status == status).ToList();

         public Order CreateOrder(Order order)
        {
            order.CreationDatetime = DateTime.Now.Date;
            _order.InsertOne(order);
            return order;
        }

         public void UpdateOrder(string id, Order orderIn) => _order.ReplaceOne(order => order.OrderId == id, orderIn);
    
    
    }
}