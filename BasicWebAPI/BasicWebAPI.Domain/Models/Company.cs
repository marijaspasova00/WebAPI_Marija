using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebAPI.Domain.Models
{
    public class Company : BaseEntity
    {
        public string CompanyName { get; set; }
        public List<Contact> Contacts { get; set; }
    }
}
