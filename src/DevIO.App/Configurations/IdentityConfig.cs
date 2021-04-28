using DevIO.App.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevIO.App.Configurations
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


            services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false; //confirmação de email
                options.SignIn.RequireConfirmedEmail = false; //confirmação de email
                options.Password.RequireDigit = false; //numero
                options.Password.RequiredLength = 6; //tamanho min
                options.Password.RequireNonAlphanumeric = false; //caracter especial
                options.Password.RequireUppercase = false; //letra maiuscula,
                options.Password.RequireLowercase = false; //letra minuscula

            })

                .AddEntityFrameworkStores<ApplicationDbContext>();

            return services;
        }
    }
}
