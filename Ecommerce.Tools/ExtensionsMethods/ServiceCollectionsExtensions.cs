using Ecommerce.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Tools.ExtensionsMethods
{
    public static class ServiceCollectionsExtensions
    {
        public static IServiceCollection AddServiceContext(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            return services.AddDbContext<DataContext>(options => 
                            options.UseSqlServer(configuration.GetConnectionString("AmazonRDSConnection")));
        }
    }
}
