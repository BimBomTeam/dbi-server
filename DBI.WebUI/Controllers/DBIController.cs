using DBI.WebUI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Buffers.Text;

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
        public async Task<IActionResult> Identify([FromBody] IdentifyDTO dto)
        {
            try
            {
                string result = service.Identify(dto.Base64);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }

        }
    }
}
