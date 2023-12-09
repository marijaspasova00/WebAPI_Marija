using BasicWebAPI.DataAccess.Interfaces;
using BasicWebAPI.Domain.Models;
using BasicWebAPI.DTOs.CompanyDTO;
using BasicWebAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicWebAPI.Mappers;

namespace BasicWebAPI.Services.Implementations
{
    public class CompanyService : ICompanyService
    {
        private readonly IRepository<Company> _companyRepository;
        public CompanyService(IRepository<Company> companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<int> CreateAsync(CompanyDto companyDto)
        {
            Company companyDb = companyDto.MapToCompany();
            await _companyRepository.CreateAsync(companyDb);
            return companyDto.Id;
        }

        public async Task DeleteAsync(int id)
        {
            await _companyRepository.DeleteAsync(id);
        }

        public async Task<List<CompanyDto>> GetAllAsync()
        {
            List<Company> companies = await _companyRepository.GetAllAsync();

            if (companies == null)
            {
                throw new Exception("Companies are null");
            }

            return companies.Select(company => company.MapToCompanyDto()).ToList();
        }

        public async Task<CompanyDto> GetByIdAsync(int id)
        {
            Company companyb = await _companyRepository.GetByIdAsync(id);

            if (companyb == null)
            {
                throw new Exception("Company is null");
            }

            return companyb.MapToCompanyDto();
        }

        public async Task<CompanyDto> UpdateAsync(CompanyDto companyDto, int id)
        {
            Company companyDb = await _companyRepository.GetByIdAsync(id);

            if(companyDb == null)
            {
                throw new Exception("Company is null!");
            }
            companyDb.Id = companyDto.Id;
            companyDb.CompanyName = companyDto.CompanyName;

            await _companyRepository.UpdateAsync(companyDb);
            return companyDto;
        }
    }
}
