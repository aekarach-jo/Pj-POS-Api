using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using _3MeePOSapi.Models;
using _3MeePOSapi.Services;
using Version = _3MeePOSapi.Models.Version;

namespace _3MeePOSapi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MessageController : ControllerBase
    {
        private readonly MessageService _messageService;
        public MessageController(MessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public Message GetMessage() => _messageService.GetMessage();

        [HttpPost]
        public Message AddMessage([FromBody] Message message)
        {
            _messageService.CreateMessage(message);
            return message;
        }

        [HttpPut("{id}")]
        public IActionResult EditMessage([FromBody] Message message, string id)
        {
            var messages = _messageService.GetMessageById(id);
            if (messages == null)
            {
                return NotFound();
            }
            message.Id = id;
            _messageService.UpdateMessage(id, message);
            return NoContent();
        }

    }
}