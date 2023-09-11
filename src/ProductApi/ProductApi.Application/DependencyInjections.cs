using Microsoft.Extensions.DependencyInjection;
using ProductApi.Application.Services;

namespace ProductApi.Application
{
    public static class DependencyInjections
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
        }
    }
}
