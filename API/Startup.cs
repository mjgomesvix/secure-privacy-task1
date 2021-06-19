using API.Base.Middlewares;
using ApplicationLayer.Configs.AutoMappers;
using ApplicationLayer.Configs.ServicesCollections;
using ApplicationLayer.OrderAppService;
using ApplicationLayer.OrderAppService.Interfaces;
using ApplicationLayer.PersonAppService;
using ApplicationLayer.PersonAppService.Interfaces;
using ApplicationLayer.ProductAppService;
using ApplicationLayer.ProductAppService.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace API
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
            services.AddControllers();

            services.AddMemoryCache();
            services.AddHttpContextAccessor();
            services.AddSingleton(Configuration);

            services.AddSingleton<IPersonAppService, PersonAppService>();
            services.AddSingleton<IProductAppService, ProductAppService>();
            services.AddSingleton<IOrderAppService, OrderAppService>();

            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Secury Privacy Test Challenge - Task 1 API", Version = "alpha" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                opt.IncludeXmlComments(xmlPath);
            });

            //Loading others Service Collections
            services.ApplicationLayerServiceCollectionLoad();

            //Automapper Config
            var mapperConfiguration = new MapperConfiguration(cfg => ApplicationLayerAutoMapper.GetMap(cfg));
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger(c => c.RouteTemplate = "swagger/{documentname}/swagger.json");

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Secury Privacy Test Challenge - Task 1 API V1");
                c.RoutePrefix = "swagger";
            });

            #region Middlewares

            app.UseRequestExceptionHandling();

            #endregion

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
