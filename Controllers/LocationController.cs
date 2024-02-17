using LocationAPI.Common;
using LocationAPI.Model;
using LocationAPI.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LocationAPI.Controllers
{
    /// <summary>
    /// Location End Point
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class LocationController : Controller
    {
        private readonly IData data;

        public LocationController(IData data)
        {
            this.data = data;
        }
        /// <summary>
        /// Get list location Data from device
        /// </summary>
        /// <param name="ft">Optional from time (format should be HH:mm)</param>
        /// <param name="tt">Optional to time (format should be HH:mm)</param>
        /// <returns>List of location</returns>
        [HttpGet("list")]
        public async Task<IActionResult> List([FromQuery] string? ft, [FromQuery] string? tt)
        {
            var result = await this.data.List(ft != null ? Utility.ToTime(ft) : null, tt!=null ? Utility.ToTime(tt) : null);
            if (result.Success) { return Ok(result); }
            else { return BadRequest(result); }
        }
        /// <summary>
        /// File Import
        /// </summary>
        /// <param name="files">List of Form File</param>
        /// <returns>Result Data Transfer Object</returns>
        [HttpPost("import")]        
        public async Task<IActionResult> Import(List<IFormFile> files)
        {
            var result = await this.data.Import(files);
            if (result.Success) { return Ok(result); }
            else { return BadRequest(result); }
        }
        /// <summary>
        /// Save single location object
        /// </summary>
        /// <param name="location">Location DTO</param>
        /// <returns>Result DTO</returns>
        [HttpPost("save")]
        public async Task<IActionResult> Save(LocationDTO location)
        {
            var result = await this.data.Save(location);
            if (result.Success) { return Ok(result); }
            else { return BadRequest(result); }
        }
        /// <summary>
        /// Delete exisitng record based on location and business
        /// </summary>
        /// <param name="location">Location DTO</param>
        /// <returns>Result DTO</returns>
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(LocationDTO location)
        {
            var result = await this.data.Delete(location);
            if (result.Success) { return Ok(result); }
            else { return BadRequest(result); }
        }
    }
}
