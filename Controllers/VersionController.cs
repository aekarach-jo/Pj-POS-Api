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
    public class VersionController : ControllerBase
    {
        private readonly VersionService _versionService;
        public VersionController(VersionService versionService)
        {
            _versionService = versionService;
        }

        [HttpGet]
        public ActionResult<Version> GetVersion() => _versionService.GetVersions();

        [HttpPost]
        public Version AddVersion([FromBody] Version version)
        {
            _versionService.CreateVersion(version);
            return version;
        }

        [HttpPut("{id}")]
        public IActionResult EditVersion([FromBody] Version version, string id)
        {
            var versions = _versionService.GetVersionsById(id);
            if (versions == null)
            {
                return NotFound();
            }
            version.VersionId = id;
            _versionService.UpdateVersion(id, version);
            return NoContent();
        }

    }
}