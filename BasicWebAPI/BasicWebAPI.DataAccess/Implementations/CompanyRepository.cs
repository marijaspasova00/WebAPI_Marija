using BasicWebAPI.DataAccess.Interfaces;
using BasicWebAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebAPI.DataAccess.Implementations
{
    public class CompanyRepository : IRepository<Company>
    {
        private readonly ProjectDbContext _dbContext;
        public CompanyRepository(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateAsync(Company entity)
        {
            await _dbContext.Companies.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteAsync(int id)
        {
            Company companyDb = await GetByIdAsync(id);
            if (companyDb != null)
            {
                _dbContext.Companies.Remove(companyDb);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                Console.WriteLine("Company is null!");
            }
        }

        public async Task<List<Company>> GetAllAsync()
        {
            return await _dbContext.Companies.ToListAsync();
        }

        public async Task<Company> GetByIdAsync(int id)
        {
            return await _dbContext.Companies
                .Include(c => c.Contacts)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Company> UpdateAsync(Company entity)
        {
            _dbContext.Companies.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
