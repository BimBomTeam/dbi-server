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

        public DBIController(IBreedIdentificationService service, IHistoryService historyService)
        {
            this.service = service;
            this.historyService = historyService;
        }

        [HttpPost("identify")]
        public async Task<IActionResult> Identify([FromBody] IdentifyDTO dto)
        {
            try
            {
                var result = await service.IdentifyAsync(dto.Base64);
                if (HttpContext.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
                {
                    if (!string.IsNullOrEmpty(authorizationHeader))
                    {
                        var userUid = await FirebaseAuthService.GetUserIdByBearerToken(authorizationHeader.ToString());
                        await historyService.AddSearchHistory(new Infrastructure.Dto.HistoryDto() 
                        { 
                            UserId = userUid, 
                            DogBreedId = result.Id 
                        });
                    }
                }
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
