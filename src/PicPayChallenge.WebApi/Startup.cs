using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using PicPayChallenge.Application.Interfaces;
using PicPayChallenge.Application.Services;
using PicPayChallenge.Core.Configs;
using PicPayChallenge.Core.Interfaces.Repositories;
using PicPayChallenge.Core.Interfaces.Services;
using PicPayChallenge.Core.Profiles;
using PicPayChallenge.Core.Services;
using PicPayChallenge.Infrastructure.Persistence.Repositories;
using PicPayChallenge.WebApi.Middlewares;
using System.Text;

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
            services.Configure<JwtConfig>(options => Configuration.GetSection("Jwt").Bind(options));


            var jwtConfig = Configuration.GetSection("Jwt").Get<JwtConfig>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJtwService, JtwService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserAuthRepository, UserAuthRepository>();

            services.AddAutoMapper(typeof(UserProfile).Assembly);

            services.AddCors();
            services.AddControllers();
            
            var key = Encoding.ASCII.GetBytes(jwtConfig.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware(typeof(ExceptionHandlerMiddleware));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
