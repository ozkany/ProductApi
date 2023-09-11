using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ProductApi.Application.Dtos;
using ProductApi.Application.Services;
using ProductApi.Application.Validators;
using System.Reflection;

namespace ProductApi.Application
{
    public static class DependencyInjections
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;

            services.AddScoped<IProductService, ProductService>();
        }
    }
}
