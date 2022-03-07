using MongoDB.Driver;
using _3MeePOSapi.Models;
using System.Collections.Generic;
using System.Linq;
using System;

// namespace _3MeePOSapi.Services
// {
//     public class ListSellItemService
//     {
//         private readonly IMongoCollection<ListSellItemService> _listSellItem;

//         public ListSellItemService(DatabaseSetting settings)
//         {
//             var Client = new MongoClient(settings.ConnectionString);
//             var database = Client.GetDatabase(settings.DatabaseName);

//             _listSellItem = database.GetCollection<ListSellItem>(settings.);
//         }


        
//     }
// }

namespace _3MeePOSapi.Services
{
    public class ListSellItemService
    {
        private readonly IMongoCollection<ListSellItem> _listSellItem;

        public ListSellItemService(DatabaseSetting settings)
        {
            var Client = new MongoClient(settings.ConnectionString);
            var database = Client.GetDatabase(settings.DatabaseName);

            _listSellItem = database.GetCollection<ListSellItem>(settings.ListSellItemCollection); 
        }

    }
}