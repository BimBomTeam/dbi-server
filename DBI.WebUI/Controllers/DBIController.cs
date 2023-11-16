using DBI.WebUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DBI.WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DBIController : ControllerBase
    {
        private readonly DogBreedIdentificationService service;

        public DBIController(DogBreedIdentificationService service)
        {
            this.service = service;
        }
        [HttpPost("identify")]
        public async Task<IActionResult> Identify([FromBody] string base64)
        {
            try
            {
                string image = service.Identify(base64);
                return Ok(image);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }

        }
    }
}
