using DBI.Infrastructure.Dto;
using DBI.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace DBI.WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DogBreedController : Controller
    {
        private readonly IDogBreedService dogBreedService;
        private readonly ILogger<DBIController> _logger;


        public DogBreedController(IDogBreedService dogBreedService, ILogger<DBIController> logger)
        {
            this.dogBreedService = dogBreedService;
            _logger = logger;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllBreedsAsync()
        {
            try
            {
                var result = dogBreedService.GetAllBreeds();
                _logger.LogInformation("All breeds downloaded " + DateTime.Now);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error during downloading all breeds " + DateTime.Now + ex);
                return BadRequest();
            }
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddDogBreedAsync([FromBody] DogBreedDto dogBreed)
        {
            try
            {
                var result = await dogBreedService.AddBreed(dogBreed);
                _logger.LogInformation("Breed added " + DateTime.Now);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error during adding breed " + DateTime.Now + ex);
                return BadRequest();
            }
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteDogBreedAsync([FromQuery] int id)
        {
            try
            {
                dogBreedService.DeleteBreed(id);
                _logger.LogInformation("Breed deleted " + DateTime.Now);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error during breed delete " + DateTime.Now + ex);
                return BadRequest();
            }
        }
        [HttpPut("edit-breed")]
        public async Task<IActionResult> EditBreed([FromBody] DogBreedDto dogBreed)
        {
            try
            {
                dogBreedService.EditBreed(dogBreed);
                _logger.LogInformation("Breed edited " + DateTime.Now);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error during edit breed " + DateTime.Now + ex);
                return BadRequest();
            }
        }
    }
}
