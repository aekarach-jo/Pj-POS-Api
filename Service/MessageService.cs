using MongoDB.Driver;
using _3MeePOSapi.Models;
using System.Collections.Generic;
using System.Linq;

namespace _3MeePOSapi.Services
{
    public class MessageService
    {
        private readonly IMongoCollection<Message> _message;

        public MessageService(DatabaseSetting settings)
        {
            var Client = new MongoClient(settings.ConnectionString);
            var database = Client.GetDatabase(settings.DatabaseName);

            _message = database.GetCollection<Message>(settings.MessageCollection);
        }

        public Message GetMessage() => _message.Find(vs => true).FirstOrDefault();
        public Message GetMessageById(string id) => _message.Find(vs => vs.Id == id).FirstOrDefault();
        public Message CreateMessage(Message message)
        {
            _message.InsertOne(message);
            return message;
        }
        public void UpdateMessage(string id, Message message) => _message.ReplaceOne(ver => ver.Id == id, message);
    }
}