using DBI.Infrastructure.Dto;
using DBI.Infrastructure.Services;
using DBI.Infrastructure.Services.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DBI.WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService authService;
        private readonly IFirebaseAuthService firebaseAuthService;
        private readonly ILogger<DBIController> _logger;

        public AuthController(IAuthService authService, ILogger<DBIController> logger, IFirebaseAuthService firebaseAuthService)
        {
            this.authService = authService;
            this._logger = logger;
            this.firebaseAuthService = firebaseAuthService;
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
        public async Task<IActionResult> RegisterFirebaseAsync([FromBody] UserCredential dto)
        {
            try
            {
                if (string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password))
                    return BadRequest("Cant be empty");
                var userId = await firebaseAuthService.SignUpAsync(dto);
                _logger.LogInformation("User registered " + DateTime.Now);
                return Ok(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error during registration " + DateTime.Now + ex);
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginFirebaseAsync([FromBody] UserCredential dto)
        {
            try
            {
                var userId = await firebaseAuthService.LoginAsync(dto);

                return Ok(userId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
