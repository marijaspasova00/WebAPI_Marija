using BasicWebAPI.DTOs.CompanyDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebAPI.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<List<CompanyDto>> GetAllAsync();
        Task<CompanyDto> GetByIdAsync(int id);
        Task<int> CreateAsync(CompanyDto companyDto);
        Task<CompanyDto> UpdateAsync(CompanyDto companyDto, int id);
        Task DeleteAsync(int id);
    }
}
