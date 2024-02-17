using LocationAPI.Common;
using LocationAPI.Model;
using LocationAPI.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LocationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : Controller
    {
        private readonly IData data;

        public LocationController(IData data)
        {
            this.data = data;
        }
        [HttpGet("list")]
        public async Task<IActionResult> List([FromQuery] string? ft, [FromQuery] string? tt)
        {
            var result = await this.data.List(ft != null ? Utility.ToTime(ft) : null, tt!=null ? Utility.ToTime(tt) : null);
            if (result.Success) { return Ok(result); }
            else { return BadRequest(result); }
        }
        [HttpPost("import")]        
        public async Task<IActionResult> Import(List<IFormFile> files)
        {
            var result = await this.data.Import(files);
            if (result.Success) { return Ok(result); }
            else { return BadRequest(result); }
        }
        [HttpPost("save")]
        public async Task<IActionResult> Save(LocationDTO location)
        {
            var result = await this.data.Save(location);
            if (result.Success) { return Ok(result); }
            else { return BadRequest(result); }
        }
    }
}
