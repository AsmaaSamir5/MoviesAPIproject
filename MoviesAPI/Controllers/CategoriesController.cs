using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _ICategoriesService;

        public CategoriesController(ICategoriesService iCategoriesService)
        {
            _ICategoriesService = iCategoriesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var cats = await _ICategoriesService.GetAllAsync();
            return Ok(cats);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CategoryDto dto)
        {
            var cat = new Category
            {
                Name = dto.Name
            };
            await _ICategoriesService.Add(cat);
            return Ok(cat);
        }

        [HttpPut("{id}")]
        //api/categories/1
        public async Task<IActionResult> UpdateAsync(byte id, [FromBody] CategoryDto dto)
        {
            var cat = await _ICategoriesService.GetById(id);
            if (cat == null)
                return NotFound($"No Category was found with id = {id}");
            cat.Name = dto.Name;

            _ICategoriesService.Update(cat);

            return Ok(cat);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(byte id)
        {
            var cat = await _ICategoriesService.GetById(id);
            if (cat == null)
                return NotFound($"No Category was found with id = {id}");

            _ICategoriesService.Delete(cat);

            return Ok(cat);
        }
    }
}
