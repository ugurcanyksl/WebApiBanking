using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiBanking.Models.Context;
using WebApiBanking.MySeed;
using WebApiBanking.Presentation;
using WebApiBanking.Repositorty;
using WebApiBanking.Repositorty.IRepository;
using WebApiBanking.Services;
using Microsoft.EntityFrameworkCore;

namespace WebApiBanking
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
            services.AddDbContext<BankCardDbContext>();
        //    services.AddDbContext<BankCardDbContext>(options =>
        //options.UseSqlServer("Server=DESKTOP-H93LQ50\\SQLEXPRESS;Database=BankDB;Integrated Security=True;"), ServiceLifetime.Scoped);
            services.AddDbContext<BankCardDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Server=DESKTOP-H93LQ50\\SQLEXPRESS;Database=BankDB;Integrated Security=True;"));
            });
            services.AddScoped<IBankCardRepository, BankCardRepository>();
            services.AddScoped<Presentation.IBankCardService, BankCardService>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApiBanking", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, BankCardDbContext dbContext)
        {
            SeedData.Initialize(dbContext);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiBanking v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
