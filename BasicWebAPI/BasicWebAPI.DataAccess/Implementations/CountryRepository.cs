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
    public class CountryRepository : IRepository<Country>
    {
        private readonly ProjectDbContext _dbContext;
        public CountryRepository(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateAsync(Country entity)
        {
            await _dbContext.Countries.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteAsync(int id)
        {
            Country countryDb = await GetByIdAsync(id);
            if (countryDb != null)
            {

                _dbContext.Countries.Remove(countryDb);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                Console.WriteLine("Country is null!");
            }
        }

        public async Task<List<Country>> GetAllAsync()
        {
            return await _dbContext.Countries.ToListAsync();
        }

        public async Task<Country> GetByIdAsync(int id)
        {
            return await _dbContext.Countries
                .Include(c => c.Contacts)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Country> UpdateAsync(Country entity)
        {
            _dbContext.Countries.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
