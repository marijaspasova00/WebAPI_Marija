using BasicWebAPI.DataAccess.Interfaces;
using BasicWebAPI.Domain.Models;
using BasicWebAPI.DTOs.CompanyDTO;
using BasicWebAPI.DTOs.ContactDTO;
using BasicWebAPI.Services.Implementations;
using BasicWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasicWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }
        [HttpGet]
        public async Task<ActionResult<List<ContactDto>>> GetAllContactsAsync()
        {
            try
            {
                return Ok(await _contactService.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Please contact the support team.");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactDto>> GetContactByIdAsync(int id)
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

                ContactDto contactDto = await _contactService.GetByIdAsync(id);

                if (contactDto == null)
                {
                    return NotFound($"Contact with Id: {id} not found");
                }

                return Ok(contactDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Please contact the support team.");
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateContactAsync([FromBody] ContactDto contactDto, int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("Invalid input");
                }

                await _contactService.UpdateAsync(contactDto, id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Please contact the support team.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteContactAsync(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("Invalid input");
                }

                await _contactService.DeleteAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Please contact the support team.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateContactAsync([FromBody] ContactDto contactDto)
        {
            try
            {
                if (contactDto == null || contactDto.Id == 0 || contactDto.ContactName == null
                    || contactDto.CompanyDto == null || contactDto.CountryDto == null)
                {
                    return BadRequest("Invalid input");
                }

                await _contactService.CreateAsync(contactDto);

                return StatusCode(StatusCodes.Status201Created, "Contact added");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Please contact the support team.");
            }
        }

        [HttpGet("withCompanyAndCountry")]
        public async Task<ActionResult<ContactDto>> GetContactsWithCompanyAndCountry()
        {
            try
            {
                ContactDto contactDto = await _contactService.GetContactsWithCompanyAndCountry();
                if (contactDto == null)
                {
                    return NotFound();
                }

                return Ok(contactDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Please contact the support team.");
            }
        }

        [HttpGet("filter")]
        public async Task<ActionResult<List<ContactDto>>> FilterContacts(int countryId, int companyId)
        {
            try
            {
                var filteredContacts = await _contactService.FilterContact(countryId, companyId);
                return Ok(filteredContacts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Please contact the support team.");
            }
        }
    }

}


