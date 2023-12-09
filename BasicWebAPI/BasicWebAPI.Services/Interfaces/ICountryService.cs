using BasicWebAPI.DTOs.CompanyDTO;
using BasicWebAPI.DTOs.CountryDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebAPI.Services.Interfaces
{
    public interface ICountryService
    {
        Task<List<CountryDto>> GetAllAsync();
        Task<CountryDto> GetByIdAsync(int id);
        Task<int> CreateAsync(CountryDto countryDto);
        Task<CountryDto> UpdateAsync(CountryDto countryDto, int id);
        Task DeleteAsync(int id);
    }
}
