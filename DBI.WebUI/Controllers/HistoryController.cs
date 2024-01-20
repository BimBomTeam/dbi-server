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

        public HistoryController(IHistoryService historyService)
        {
            this.historyService = historyService;
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
                        return Ok(result);
                    }
                }
                return BadRequest("User are not authorized");
            }
            catch (Exception ex)
            {
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
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteSearchHistory([FromQuery] int id)
        {
            try
            {
                historyService.DeleteSearchHistory(id);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
