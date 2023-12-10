using DBI.Application.Services;
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

        public DogBreedController(IDogBreedService dogBreedService)
        {
            this.dogBreedService = dogBreedService;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllBreedsAsync()
        {
            try
            {
                var result = dogBreedService.GetAllBreeds();
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddDogBreedAsync([FromBody] DogBreedDto dogBreed)
        {
            try
            {
                var result = await dogBreedService.AddBreed(dogBreed);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteDogBreedAsync([FromQuery] int id)
        {
            try
            {
                dogBreedService.DeleteBreed(id);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateBreedAsync([FromBody] DogBreedDto dogBreed)
        {
            try
            {
                dogBreedService.UpdateBreed(dogBreed);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
