using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POS.Application.Interfaces;
using POS.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Extensions
{
    public static class InjectionExtension
    {
        public static IServiceCollection AddInjectionApplication(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddSingleton(configuration);

            service.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            service.AddAutoMapper(Assembly.GetExecutingAssembly());

            service.AddScoped<ICategoryApplication, CategoryApplication>();
            service.AddScoped<IUserApplication, UserApplication>();
            service.AddScoped<IProviderApplication, ProviderApplication>();

            return service;
        }
    }
}
