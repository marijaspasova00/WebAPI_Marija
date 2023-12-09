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
    public class ContactRepository : IContactRepository
    {
        private readonly ProjectDbContext _dbContext;
        public ContactRepository(ProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> CreateAsync(Contact entity)
        {
            await _dbContext.Contacts.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteAsync(int id)
        {
            Contact contactDb = await GetByIdAsync(id);

            if(contactDb != null)
            {
                _dbContext.Contacts.Remove(contactDb);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                Console.WriteLine("Contact is null!");
            }
        }

        public async Task<List<Contact>> FilterContact(int countryId, int companyId)
        {
            var contacts = await _dbContext.Contacts
                    .Include(c => c.Company)
                    .Include(c => c.Country)
                    .Where(c => c.CountryId == countryId && c.CompanyId == companyId)
                    .ToListAsync();
            return contacts;
        }

        public async Task<List<Contact>> GetAllAsync()
        {
            return await _dbContext.Contacts
                .Include(c => c.Company)
                .Include(c => c.Country)
                .ToListAsync();
        }

        public async Task<Contact> GetByIdAsync(int id)
        {
            return await _dbContext.Contacts.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Contact> GetContactsWithCompanyAndCountry()
        {
            return await _dbContext.Contacts
                   .Include(c => c.Company)
                   .Include(c => c.Country)
                   .FirstOrDefaultAsync();
        }

        public async Task<Contact> UpdateAsync(Contact entity)
        {
            _dbContext.Contacts.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

    }
}
