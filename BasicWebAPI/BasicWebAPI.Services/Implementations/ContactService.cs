using BasicWebAPI.DataAccess.Implementations;
using BasicWebAPI.DataAccess.Interfaces;
using BasicWebAPI.Domain.Models;
using BasicWebAPI.DTOs.CompanyDTO;
using BasicWebAPI.DTOs.ContactDTO;
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
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        public ContactService (IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<int> CreateAsync(ContactDto contactDto)
        {
            Contact contactDb = contactDto.MapToContact();
            await _contactRepository.CreateAsync(contactDb);
            return contactDb.Id;
        }

        public async Task DeleteAsync(int id)
        {
            await _contactRepository.DeleteAsync(id);
        }

        public async Task<List<ContactDto>> FilterContact(int countryId, int companyId)
        {
            var contacts = await _contactRepository.FilterContact(countryId, companyId);

            var contactDtos = contacts.Select(contact => contact.MapToContactDto()).ToList();
            return contactDtos;
        }

        public async Task<List<ContactDto>> GetAllAsync()
        {
            List<Contact> contacts = await _contactRepository.GetAllAsync();

            if (contacts == null)
            {
                throw new Exception("Contacts are null");
            }

            return contacts.Select(contact => contact.MapToContactDto()).ToList();
        }

        public async Task<ContactDto> GetByIdAsync(int id)
        {
            Contact contact = await _contactRepository.GetContactsWithCompanyAndCountry();

            if (contact == null)
            {
                throw new Exception("Contact is null");
            }

            return contact.MapToContactDto();
        }

        public async Task<ContactDto> GetContactsWithCompanyAndCountry()
        {
            Contact contact = await _contactRepository.GetContactsWithCompanyAndCountry();
            ContactDto contactDto = contact.MapToContactDto();

            return contactDto;
        }

        public async Task<CompanyDto> UpdateAsync(ContactDto contactDto, int id)
        {
            Contact contactDb = await _contactRepository.GetByIdAsync(id);


            if (contactDb == null)
            {
                throw new Exception("Contact is null!");
            }
            contactDb.ContactName = contactDto.ContactName;
            contactDb.Company = contactDto.CompanyDto.MapToCompany();
            contactDb.Country = contactDto.CountryDto.MapToCountry();

            await _contactRepository.UpdateAsync(contactDb);
            var updatedContact = await _contactRepository.GetByIdAsync(id);
            return updatedContact.Company.MapToCompanyDto();
        }
    }
}
