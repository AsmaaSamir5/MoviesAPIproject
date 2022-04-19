using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMapper _Mapper;
        private readonly IMoviesService _MoviesService;
        private readonly ICategoriesService _ICategoriesService;
        private new List<string> _allowedExtensions = new List<string> { ".jpg", ".png" };
        private long _maxAllowedPosterSize = 1048576;

        public MoviesController(IMoviesService MoviesService, ICategoriesService iCategoriesService, IMapper mapper)
        {
            _MoviesService = MoviesService;
            _ICategoriesService = iCategoriesService;
            _Mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var movies = await _MoviesService.GetAll();
            var data = _Mapper.Map<IEnumerable<MovieDetailsDto>>(movies);
                
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var movie = await _MoviesService.GetById(id);
            if (movie == null)
                return NotFound();

            var dto = _Mapper.Map<MovieDetailsDto>(movie);

            return Ok(dto);
        }

        [HttpGet("GetByCategotyId")]
        public async Task<IActionResult> GetByCategotyIdAsync(byte catId)
        {
            var movies = await _MoviesService.GetAll(catId);
            var data = _Mapper.Map<IEnumerable<MovieDetailsDto>>(movies);

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] MoviesDto dto)
        {

            if (dto.Poster == null)
                return BadRequest("Poster is Required!");

            if (!_allowedExtensions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("only .png and jpg images are allowed");
            if(dto.Poster.Length > _maxAllowedPosterSize)
                return BadRequest("Max allowed Size for Poster is 1MB");

            var isValidCategory = await _ICategoriesService.IsValidCategory(dto.CategoryId);
            if(!isValidCategory)
                return BadRequest("IsValid Category Id!");

            using var dataStream = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStream);
            var movie = _Mapper.Map<Movie>(dto);
            movie.Poster = dataStream.ToArray();
            _MoviesService.Add(movie);

            return Ok(movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] MoviesDto dto)
        {

            var movie = await _MoviesService.GetById(id);
            if (movie == null)
                return NotFound($"No Movie Was Found With ID = {id}");


            var isValidCategory = await _ICategoriesService.IsValidCategory(dto.CategoryId);
            if (!isValidCategory)
                return BadRequest("IsValid Category Id!");

            if(dto.Poster != null)
            {
                if (!_allowedExtensions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest("only .png and jpg images are allowed");

                if (dto.Poster.Length > _maxAllowedPosterSize)
                    return BadRequest("Max allowed Size for Poster is 1MB");

                using var dataStream = new MemoryStream();
                await dto.Poster.CopyToAsync(dataStream);

                movie.Poster = dataStream.ToArray();
            }


            movie.Title = dto.Title;
            movie.StoreLine = dto.StoreLine;
            movie.Rate = dto.Rate;
            movie.Year = dto.Year;
            movie.CategoryId = dto.CategoryId;

            _MoviesService.Update(movie);
            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var movie = await _MoviesService.GetById(id);
            if (movie == null)
                return NotFound($"No Movie Was Found With ID = {id}");

            _MoviesService.Delete(movie);

            return Ok(movie);
        }
    }
}
