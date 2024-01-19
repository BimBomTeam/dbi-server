using DBI.Infrastructure.Dto;
using DBI.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace DBI.WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService authService;
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = authService.GetAllUsers();
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegisterDto dto)
        {
            try
            {
                if (string.IsNullOrEmpty(dto.UserUId) || string.IsNullOrEmpty(dto.Identifier))
                    return BadRequest("Cant be empty");
                var id = await authService.AddUserAsync(dto);

                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
