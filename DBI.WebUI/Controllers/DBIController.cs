using DBI.WebUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DBI.WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DBIController : ControllerBase
    {
        readonly private ImageService imageService;

        public DBIController(ImageService service)
        {
            imageService = service;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] int number1)
        {
            try
            {
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Bad format");
                throw;
            }

        }
    }
}
