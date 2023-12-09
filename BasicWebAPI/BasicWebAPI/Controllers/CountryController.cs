using BasicWebAPI.DTOs.CompanyDTO;
using BasicWebAPI.DTOs.CountryDTO;
using BasicWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasicWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;
        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }
        [HttpGet]
        public async Task<ActionResult<List<CountryDto>>> GetAllCountriesAsync()
        {
            try
            {
                return Ok(await _countryService.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Please contact the support team.");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> GetCountryByIdAsync(int id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("Id can not be null");
                }

                if (id <= 0)
                {
                    return BadRequest("Invalid input for Id");
                }

                CountryDto countryDto = await _countryService.GetByIdAsync(id);

                if (countryDto == null)
                {
                    return NotFound($"Country with Id: {id} not found");
                }

                return Ok(countryDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Please contact the support team.");
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateCountryAsync([FromBody] CountryDto countryDto, int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("Invalid input");
                }

                await _countryService.UpdateAsync(countryDto, id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Please contact the support team.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCountryAsync(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("Invalid input");
                }

                await _countryService.DeleteAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Please contact the support team.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateCountryAsync([FromBody] CountryDto countryDto)
        {
            try
            {
                if (countryDto == null || countryDto.Id == 0 || countryDto.CountryName == null)
                {
                    return BadRequest("Invalid input");
                }

                await _countryService.CreateAsync(countryDto);

                return StatusCode(StatusCodes.Status201Created, "Country added");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Please contact the support team.");
            }
        }

    }
}

