using BP.Api.Service;
using BP.API.Extensions;
using BP.API.Models;
using BP.API.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

namespace BP.API
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
            services.AddControllers()
                .AddFluentValidation(fv => 
                { fv.RegisterValidatorsFromAssemblyContaining<Startup>();
                 //   fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                });
            services.ConfigureMapping();
            
            services.AddHealthChecks();
            services.AddTransient<IValidator<ContactDVO>, ContactValidator>();
            services.AddScoped<IContactService, ContactService>();
            services.AddHttpClient("garantiapi", config =>
             {
                 config.BaseAddress = new Uri("garanti.com");
                 config.DefaultRequestHeaders.Add("Authorzation", "Bearer 123");
             });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BP.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BP.API v1"));
            }

            app.UseResponseCaching();
            app.UseHttpsRedirection();

            app.UseCustomHealthCheck();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
