using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OBT.Products.API.Service;
using OBT.Products.Domain.Repositories;
using OBT.Products.Infrastructure.Data;
using OBT.Products.Infrastructure.Data.Repositories;

namespace OBT.Products.API.Configuration
{
    public static class DependencyInjectionConfig
    {

        public static IServiceCollection AddDependencyConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CatalogDbContext>(options => options.UseInMemoryDatabase("ProductsDB"));

            services.AddTransient<ProductPopulateService>();

            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}