using AutoMapper;
using Alphasource.Libs.Promocodes.Repositories;
using Alphasource.Libs.Promocodes.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;

namespace Alphasource.Libs.Promocode
{
    public class Startup
    {

        private readonly IWebHostEnvironment _hostingEnvironment;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            ConfigHelper.ContentRootPath = env.ContentRootPath;
            Configuration = ConfigHelper.Configurations();
            _hostingEnvironment = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Configuration for MongoDB
            services.Configure<Settings>(
           options =>
           {
               options.ConnectionString = Configuration.GetValue<string>("MongoDB:ConnectionString");
               options.Database = Configuration.GetValue<string>("MongoDB:Database");
           });
          
            services.AddSingleton<IMongoClient, MongoClient>(
            _ => new MongoClient(Configuration.GetValue<string>("MongoDB:ConnectionString")));

            services.AddOptions();

           // services.AddScoped<IServiceConfig, ServiceConfig>();
           // services.Configure<ServiceConfig>(Configuration.GetSection("ServiceConfig"));

            services.AddMvc();
            services.AddConfigurationSetting(Configuration);
            services.AddRepositories(Configuration);
            services.AddServices(Configuration);



            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "AlphaSource API",
                    Description = "AlphaSource API - with swagger"
                });
            });

            //Automapper
            services.AddAutoMapper(typeof(Startup));

            services.AddControllersWithViews();
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseCors("MyPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Payment}/{action=FromPostPayment}");
            });


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Alphasource V1");
            });
        }
    }
}
