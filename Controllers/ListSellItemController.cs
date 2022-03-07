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
    public class ListSellItemController : ControllerBase
    {
        private readonly ListSellItemService _listSellItemService;
        public ListSellItemController(ListSellItemService listSellItemService)
        {
            _listSellItemService = listSellItemService;
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