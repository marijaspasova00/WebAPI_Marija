using BasicWebAPI.DataAccess;
using BasicWebAPI.DataAccess.Implementations;
using BasicWebAPI.DataAccess.Interfaces;
using BasicWebAPI.Domain.Models;
using BasicWebAPI.Services.Implementations;
using BasicWebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebAPI.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(this IServiceCollection services)
        {
            services.AddDbContext<ProjectDbContext>(options => options.UseSqlServer(@"Data Source=(localdb)\SEDCLocalDb;Database=basic-web-api-db;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));
        }
        public static void InjectRepositories(this IServiceCollection services)
        {
            services.AddTransient<IRepository<Company>, CompanyRepository>();
            services.AddTransient<IRepository<Country>, CountryRepository>();
            services.AddTransient<IContactRepository, ContactRepository>();
        }
        public static void InjectServices(this IServiceCollection services)
        {
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IContactService, ContactService>();
        }
    }
}
