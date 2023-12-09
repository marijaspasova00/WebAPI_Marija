using BasicWebAPI.DataAccess.Implementations;
using BasicWebAPI.DataAccess.Interfaces;
using BasicWebAPI.Domain.Models;
using BasicWebAPI.DTOs.CompanyDTO;
using BasicWebAPI.DTOs.CountryDTO;
using BasicWebAPI.Mappers;
using BasicWebAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebAPI.Services.Implementations
{
    public class CountryService : ICountryService
    {
        private readonly IRepository<Country> _countryRepository;
        public CountryService(IRepository<Country> countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<int> CreateAsync(CountryDto countryDto)
        {
            Country countryDb = countryDto.MapToCountry();
            await _countryRepository.CreateAsync(countryDb);
            return countryDb.Id;
        }

        public async Task DeleteAsync(int id)
        {
            await _countryRepository.DeleteAsync(id);
        }

        public async Task<List<CountryDto>> GetAllAsync()
        {
            List<Country> countries = await _countryRepository.GetAllAsync();

            if(countries == null)
            {
                throw new Exception("Countries are null");
            }

            return countries.Select(country => country.MapToCountryDto()).ToList();
        }

        public async Task<CountryDto> GetByIdAsync(int id)
        {
           Country country = await _countryRepository.GetByIdAsync(id);
            if(country == null)
            {
                throw new Exception("Country is null!");
            }
            return country.MapToCountryDto();
        }

        public async Task<CountryDto> UpdateAsync(CountryDto countryDto, int id)
        {
            Country countryDb = await _countryRepository.GetByIdAsync(id);

            if (countryDb == null)
            {
                throw new Exception("Country is null!");
            }
            countryDb.Id = countryDto.Id;
            countryDb.CountryName = countryDto.CountryName;

            await _countryRepository.UpdateAsync(countryDb);
            return countryDb.MapToCountryDto();
        }
    }
}
