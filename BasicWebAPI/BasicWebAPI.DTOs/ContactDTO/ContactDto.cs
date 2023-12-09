using BasicWebAPI.DTOs.CompanyDTO;
using BasicWebAPI.DTOs.CountryDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebAPI.DTOs.ContactDTO
{
    public class ContactDto
    {
        public int Id { get; set; }
        public string ContactName { get; set; }
        public CompanyDto CompanyDto { get; set; }
        public CountryDto CountryDto { get; set; }

    }
}
