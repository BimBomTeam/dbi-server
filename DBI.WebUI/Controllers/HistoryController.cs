using DBI.Application.Services;
using DBI.Infrastructure.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;



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
        public async Task<ActionResult> GetSearchHistory()
        {
            try
            {
                var result = historyService.GetSearchHistory();
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddSearchHistoryAsync([FromBody] HistoryDto historyEntityDto, [FromHeader(Name = "Authorization")] string authToken)
        {
            try
            {
                string userUid = DecodeAuthToken(authToken);
                var result = historyService.AddSearchHistory(historyEntityDto);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
        private string DecodeAuthToken(string authToken)
        {
            var key = Encoding.ASCII.GetBytes("nC4HGoTRMvgUAU52eHmhEMaQdpmpEwCj0wp6NdGbfqk");

            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            try
            {
                
                var claimsPrincipal = tokenHandler.ValidateToken(authToken, validationParameters, out var validatedToken);

                
                if (validatedToken is JwtSecurityToken jwtSecurityToken && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    var uidClaim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);
                    if (uidClaim != null)
                    {
                        return uidClaim.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Błąd walidacji tokenu: {ex.Message}");
            }

            
            return null;
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
