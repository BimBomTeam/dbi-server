using DBI.Application.Services;
using DBI.Application.Services.Authorization;
using DBI.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Text;

namespace DBI.WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DBIController : ControllerBase
    {
        private readonly IBreedIdentificationService service;
        private readonly IHistoryService historyService;
        private readonly ILogger<DBIController> _logger;

        public DBIController(IBreedIdentificationService service, IHistoryService historyService, ILogger<DBIController> logger)
        {
            this.service = service;
            this.historyService = historyService;
            this._logger = logger;
        }

        [HttpPost("identify")]
        public async Task<IActionResult> Identify([FromBody] IdentifyDTO dto)
        {
            try
            {
                var result = await service.IdentifyAsync(dto.Base64);
                _logger.LogInformation("Breed recognized " + DateTime.Now);

                if (result.Id != -1 && HttpContext.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
                {
                    if (!string.IsNullOrEmpty(authorizationHeader))
                    {
                        var userUid = await FirebaseAuthService.GetUserIdByBearerToken(authorizationHeader.ToString());
                        await historyService.AddSearchHistory(new Infrastructure.Dto.HistoryDto() 
                        { 
                            UserId = userUid, 
                            DogBreedId = result.Id 
                        });
                        _logger.LogInformation("Add history " + userUid);
                    }
                }

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
