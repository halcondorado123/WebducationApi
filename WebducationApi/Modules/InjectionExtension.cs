using WebducationApi.Application.Interface;
using WebducationApi.Domain.Core;
using WebducationApi.Infraestructure.Data;
using WebducationApi.Domain.Interface;
using WebducationApi.Infraestructure.Interface;
using WebducationApi.Infraestructure.Repository;
using WebducationApi.Tranversal.Mapper;

namespace WebducationApi.Modules
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<ApiDbContext>();
            services.AddAutoMapper(typeof(MappingsProfile)); // si está en el mismo assembly
            services.AddScoped<IStudentApplication, StudentApplication>();
            services.AddScoped<IStudentDomain, StudentDomain> ();
            services.AddScoped<IStudentRepository, StudentRepository>();

            return services;
        }
    }
}
