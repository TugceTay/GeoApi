using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EfCore;
using Services;
using Services.Contracts;

namespace _5_LayeredArchitecture.Extensions
{
        public static class ServicesExtensions
        {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration
      ) => services.AddDbContext<RepositoryContext>(options =>
          options.UseNpgsql(configuration.GetConnectionString("sqlConnection"), prj => {
              prj.UseNetTopologySuite(); 
          }));

        //veri depolarına erişim sağlamak için gerekli olan bir hizmeti yapılandırmak için kullanılır
        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
                services.AddScoped<IRepositoryManager, RepositoryManager>();

            public static void ConfigureServiceManager(this IServiceCollection services) =>
                services.AddScoped<IServiceManager, ServiceManager>();

        }
}

