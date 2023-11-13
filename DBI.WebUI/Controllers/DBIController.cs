using Microsoft.AspNetCore.Mvc;

namespace DBI.WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DBIController : ControllerBase
    {
        [HttpGet("test")]
        
        public async Task<IActionResult> Test()
        {
            int number = 5;
            return Ok(number);
        }
        
    }
}
