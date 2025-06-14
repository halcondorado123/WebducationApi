using WebducationApi.Application.Interface;
using WebducationApi.Domain.Core;
using WebducationApi.Infraestructure.Data;
using WebducationApi.Domain.Interface;
using WebducationApi.Infraestructure.Interface;
using WebducationApi.Infraestructure.Repository;
using WebducationApi.Tranversal.Mapper;
using WebducationApi.Application.Main;

namespace WebducationApi.Modules
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<ApiDbContext>();
            services.AddScoped<JWTExtension>();
            services.AddAutoMapper(typeof(MappingsProfile));

            services.AddScoped<IStudentApplication, StudentApplication>();
            services.AddScoped<IStudentDomain, StudentDomain> ();
            services.AddScoped<IStudentRepository, StudentRepository>();

            services.AddScoped<ITeacherApplication, TeacherApplication>();
            services.AddScoped<ITeacherDomain, TeacherDomain>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();


            return services;
        }
    }
}
