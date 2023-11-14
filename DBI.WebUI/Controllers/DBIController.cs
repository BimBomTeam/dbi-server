using DBI.WebUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DBI.WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DBIController : ControllerBase
    {
        [HttpPost("identify")]
        public async Task<IActionResult> Identify([FromBody] string base64)
        {
            try
            {
                string scaledBase64 = ImageHelper.ScaleImage(base64);               
                return Ok(scaledBase64);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }

        }
    }
}
