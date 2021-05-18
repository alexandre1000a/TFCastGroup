using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Swashbuckle.Swagger;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFCastGroup.Infra.Configuration;
using TFCastGroup.Service;
using TFCastGroup.Service.Interface;
using TFCastGroup.Domain.Interface;
using TFCastGroup.Infra.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;

namespace TFCastGroup.Api
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
            services.AddAuthentication(IISDefaults.AuthenticationScheme);
            services.AddControllers();
            services.AddDbContext<ContextCastGroup>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

            services.AddScoped<ICursoService, CursoService>();
            services.AddScoped<ICursoRepository, CursoRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddMvcCore();

            var basePath = PlatformServices.Default.Application.ApplicationBasePath;
            var xmlPath = Path.Combine(basePath, "TFCastGroup.Api.xml");
            

            services.AddSwaggerGen(options =>
            {
                options.IncludeXmlComments(xmlPath);
                options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
                {

                    Title = "Cursos Cast Group",
                    Version = "v2",
                    Description = "Serviço que disponibiliza endpoints de cursos.",
                });
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            

            app.UseRouting();


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(options => options.RoutePrefix = string.Empty);
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v2/swagger.json", "API Cursos"));
        }
    }
}
