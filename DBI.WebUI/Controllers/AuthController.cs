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
        public AuthController(IAuthService authService, IFirebaseAuthService firebaseAuthService)
        {
            this.authService = authService;
            this.firebaseAuthService = firebaseAuthService;
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

        //[HttpPost("register")]
        //public async Task<IActionResult> RegisterAsync([FromBody] UserRegisterDto dto)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(dto.UserUId) || string.IsNullOrEmpty(dto.Identifier))
        //            return BadRequest("Cant be empty");
        //        var id = await authService.AddUserAsync(dto);

        //        return Ok(id);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        [HttpPost("register")]
        public async Task<IActionResult> RegisterFirebaseAsync([FromBody] UserCredential dto)
        {
            try
            {
                var userId = await firebaseAuthService.SignUpAsync(dto);

                return Ok(userId);
            }
            catch (Exception ex)
            {
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
