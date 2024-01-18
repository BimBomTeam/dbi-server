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
        private readonly ILogger<DBIController> _logger;


        public AuthController(IAuthService authService, ILogger<DBIController> logger)
        {
            this.authService = authService;
            _logger = logger;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = authService.GetAllUsers();
                _logger.LogInformation("All users downloaded " + DateTime.Now);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError("Error retrieving all users " + DateTime.Now + e);
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
                _logger.LogInformation("User registered " + DateTime.Now);
                return Ok(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error during registration " + DateTime.Now + ex);
                return BadRequest(ex.Message);
            }
        }

    }
}
