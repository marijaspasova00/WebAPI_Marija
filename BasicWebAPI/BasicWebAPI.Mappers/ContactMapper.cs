using BasicWebAPI.Domain.Models;
using BasicWebAPI.DTOs.ContactDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebAPI.Mappers
{
    public static class ContactMapper
    {
        public static ContactDto MapToContactDto(this Contact contact)
        {
            if (contact == null)
            {
                return null; 
            }

            var contactDto = new ContactDto()
            {
                Id = contact.Id,
                ContactName = contact.ContactName,
            };

            if (contact.Company != null || contact.Country != null)
            {
                contactDto.CompanyDto = contact.Company.MapToCompanyDto();
                contactDto.CountryDto = contact.Country.MapToCountryDto();
            }
            return contactDto;
        }
        public static Contact MapToContact(this ContactDto contactDto)
        {
            return new Contact()
            {
                ContactName = contactDto.ContactName,
                Company = contactDto.CompanyDto.MapToCompany(),
                Country = contactDto.CountryDto.MapToCountry()
            };
        }
    }
}
