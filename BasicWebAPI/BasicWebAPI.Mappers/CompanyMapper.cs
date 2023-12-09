using BasicWebAPI.Domain.Models;
using BasicWebAPI.DTOs.CompanyDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebAPI.Mappers
{
    public static class CompanyMapper
    {
        public static CompanyDto MapToCompanyDto (this Company company)
        {
            return new CompanyDto()
            {
                Id = company.Id,
                CompanyName = company.CompanyName
            };
        }
        public static Company MapToCompany ( this CompanyDto companyDto)
        {
            return new Company()
            {
                CompanyName = companyDto.CompanyName
            };
        }
    }
}
