using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PicPayChallenge.Application.Interfaces;
using PicPayChallenge.Application.Services;
using PicPayChallenge.Core.Configs;
using PicPayChallenge.Core.Interfaces.Repositories;
using PicPayChallenge.Core.Profiles;
using PicPayChallenge.Infrastructure.Persistence.Repositories;
using PicPayChallenge.WebApi.Middlewares;

namespace PicPayChallenge.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ConnectionStringConfig>(options => Configuration.GetSection("ConnectionStrings").Bind(options));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddAutoMapper(typeof(UserProfile).Assembly);

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseMiddleware(typeof(ExceptionHandlerMiddleware));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
