using BasicWebAPI.Domain.Models;
using BasicWebAPI.DTOs.CompanyDTO;
using BasicWebAPI.DTOs.ContactDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebAPI.Services.Interfaces
{
    public interface IContactService
    {
        Task<List<ContactDto>> GetAllAsync();
        Task<ContactDto> GetByIdAsync(int id);
        Task<int> CreateAsync(ContactDto contactDto);
        Task<CompanyDto> UpdateAsync(ContactDto contactDto, int id);
        Task DeleteAsync(int id);
        Task<ContactDto> GetContactsWithCompanyAndCountry();
        Task<List<ContactDto>> FilterContact(int countryId, int companyId);
    }
}
