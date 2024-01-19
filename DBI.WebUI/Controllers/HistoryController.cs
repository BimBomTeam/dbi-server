using DBI.Application.Services;
using DBI.Infrastructure.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

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

        [HttpGet("get-all")]
        [Authorize]
        public async Task<ActionResult> GetSearchHistory([FromHeader(Name = "Authorization")] string authToken)
        {
            try
            {
                var userId = AuthService.DecodeAuthToken(authToken);
                var result = historyService.GetSearchHistoryByUser(userId);
                return Ok(result);
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
