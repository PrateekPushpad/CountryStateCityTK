using DAL;
using DAL.Implementations;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace UI.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(options => 
            options.UseSqlServer(config.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("UI")));

            services.AddScoped<ICountryRepo, CountryRepo>();
            services.AddScoped<IStateRepo, StateRepo>();
            services.AddScoped<ICityRepo, CityRepo>();
            services.AddScoped<ISkillRepo, SkillRepo>();
            services.AddScoped<IStudentRepo, StudentRepo>();
            services.AddScoped<IUserInfoRepo, UserInfoRepo>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services;
        }
    }
}
