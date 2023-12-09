using BasicWebAPI.DTOs.CompanyDTO;
using BasicWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasicWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        [HttpGet]
        public async Task<ActionResult<List<CompanyDto>>> GetAllCompaniesAsync()
        {
            try
            {
                return Ok(await _companyService.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Please contact the support team.");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyDto>> GetCompanyByIdAsync(int id)
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

                CompanyDto companyDto = await _companyService.GetByIdAsync(id);

                if (companyDto == null)
                {
                    return NotFound($"Company with Id: {id} not found");
                }

                return Ok(companyDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Please contact the support team.");
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateCompanyAsync([FromBody] CompanyDto companyDto, int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("Invalid input");
                }

                await _companyService.UpdateAsync(companyDto, id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Please contact the support team.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCompanyAsync(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("Invalid input");
                }

                await _companyService.DeleteAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Please contact the support team.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateCompanyAsync([FromBody] CompanyDto companyDto)
        {
            try
            {
                if (companyDto == null || companyDto.Id == 0 || companyDto.CompanyName == null)
                {
                    return BadRequest("Invalid input");
                }

                await _companyService.CreateAsync(companyDto);

                return StatusCode(StatusCodes.Status201Created, "Company added");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Please contact the support team.");
            }
        }

    }
}
