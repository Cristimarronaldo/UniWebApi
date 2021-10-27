using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Unimed.API.Domain;
using Unimed.API.Domain.Interfaces;
using Unimed.API.Models;
using Unimed.API.Repositorio;
using Unimed.API.Repositorio.Data;

namespace Unimed.API
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
            services.AddDbContext<UnimedContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IPlanoDomain, PlanoDomain>();
            services.AddScoped<IPlanoRepository, PlanoRepository>();
            services.AddScoped<IClienteDomain, ClienteDomain>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IExameRepository, ExameRepository>();
            services.AddScoped<IExameDomain, ExameDomain>();
            services.AddScoped<IClienteExameRepository, ClienteExameRepository>();
            services.AddScoped<IClienteExameDomain, ClienteExameDomain>();

            services.AddControllers()
            .AddJsonOptions(opts => { opts.JsonSerializerOptions.PropertyNamingPolicy = null;                
                opts.JsonSerializerOptions.MaxDepth = 0;
                opts.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
                opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });

            //services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Unimed.API", Version = "v1" });
            });
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Unimed.API v1"));
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
