
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository.Models;
using Repository.Repositories;
using Service.Authenticators;
using Service.Interfaces;
using Service.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KitchenWeb
{
    public class Startup
    {
       
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddApplicationPart(Assembly.Load("KitchenWeb")).AddControllersAsServices();
            services.AddDbContext<InTheKitchenDBContext>(option =>
            {
                option.UseInMemoryDatabase("TestDb"+ Guid.NewGuid());
            },ServiceLifetime.Singleton);
            services.AddScoped<ILogicKitchen, KitchenLogic>();
            services.AddScoped<IReviewStepTagLogic, ReviewStepTagLogic>();
            services.AddScoped<IUserLogic, UserLogic>();
            services.AddScoped<IAuthenticator, Authenticator>();
            services.AddScoped<KitchenRepository>();
            services.AddScoped<TestLogic>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

