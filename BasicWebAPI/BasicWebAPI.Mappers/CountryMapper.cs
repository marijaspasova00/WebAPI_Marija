using BasicWebAPI.Domain.Models;
using BasicWebAPI.DTOs.CompanyDTO;
using BasicWebAPI.DTOs.CountryDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebAPI.Mappers
{
    public static class CountryMapper
    {
        public static CountryDto MapToCountryDto(this Country country)
        {
            return new CountryDto()
            {
                Id = country.Id,
                CountryName = country.CountryName
            };
        }
        public static Country MapToCountry(this CountryDto countryDto)
        {
            return new Country()
            {
                CountryName = countryDto.CountryName,
            };
        }
    }
}
