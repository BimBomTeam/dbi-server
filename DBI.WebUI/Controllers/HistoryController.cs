using DBI.Application.Services;
using DBI.Infrastructure.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using FirebaseAdmin.Auth;
using DBI.Application.Services.Authorization;

namespace DBI.WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryService historyService;
        private readonly ILogger<DBIController> _logger;


        public HistoryController(IHistoryService historyService, ILogger<DBIController> logger)
        {
            this.historyService = historyService;
            _logger = logger;
        }

        [Authorize]
        [HttpGet("get-all")]
        public async Task<ActionResult> GetSearchHistory()
        {
            try
            {
                if (HttpContext.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
                {
                    if (!string.IsNullOrEmpty(authorizationHeader))
                    {
                        var userUid = await FirebaseAuthService.GetUserIdByBearerToken(authorizationHeader.ToString());
                        var result = historyService.GetSearchHistoryByUser(userUid);
                        _logger.LogInformation("History loaded " + DateTime.Now);
                        return Ok(result);
                    }
                }
                return BadRequest("User are not authorized");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error loading history " + DateTime.Now + ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddSearchHistoryAsync([FromBody] HistoryDto historyEntityDto, [FromHeader(Name = "Authorization")] string authToken)
        {
            try
            {
                string userUid = AuthService.DecodeAuthToken(authToken);
                var result = historyService.AddSearchHistory(historyEntityDto);
                _logger.LogInformation("History record added " + DateTime.Now);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError("Error adding history record " + DateTime.Now + e);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteSearchHistory([FromQuery] int id)
        {
            try
            {
                historyService.DeleteSearchHistory(id);
                _logger.LogInformation("History record deleted " + DateTime.Now);
                return Ok();
            }
            catch (Exception exc)
            {
                _logger.LogError("Error deleting history record " + DateTime.Now + exc);
                return BadRequest();
            }
        }
    }
}
