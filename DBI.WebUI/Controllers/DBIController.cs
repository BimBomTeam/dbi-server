using DBI.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace DBI.WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DBIController : ControllerBase
    {
        private readonly IBreedIdentificationService service;
        private readonly ILogger<DBIController> _logger;


        public DBIController(IBreedIdentificationService service, ILogger<DBIController> logger)
        {
            this.service = service;
            _logger = logger;
        }

        [HttpPost("identify")]
        public async Task<IActionResult> Identify([FromBody] IdentifyDTO dto)
        {
            try
            {
                var result = await service.IdentifyAsync(dto.Base64);
                _logger.LogInformation("Breed recognized " + DateTime.Now);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in breed recognition " + DateTime.Now + ex);
                return BadRequest(ex.Message);
                throw;
            }
        }
    }
}
