using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebAPI.Domain.Models
{
    public class Country : BaseEntity
    {
        public string CountryName { get; set; }
        public List<Contact> Contacts { get; set; }
    }
}
