using System;
using MongoDB.Bson.Serialization.Attributes;

namespace _3MeePOSapi.Models
{
   public class Order
    {
        [BsonId]
        public string OrderId { get; set; }
        public string Status { get; set; }
        public string UserId { get; set; }
        public string Address { get; set; }
        public string Slip { get; set; }
        public ListSellItem[] ListSellItem { get; set; }
        public int TotalPrice { get; set; }
        public DateTime? CreationDatetime { get; set; }
    }
}