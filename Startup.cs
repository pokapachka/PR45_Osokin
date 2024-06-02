using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ПР45_Осокин
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Руководство для пользования запросов",
                    Description = "Полное руководство для использования запросов находящихся в проекте"
                });
                c.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v2",
                    Title = "Руководство для пользования запросов",
                    Description = "Полное руководство для использования запросов находящихся в проекте"
                });
                c.SwaggerDoc("v3", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v3",
                    Title = "Руководство для пользования запросов",
                    Description = "Полное руководство для использования запросов находящихся в проекте"
                });
                var filePath = Path.Combine(System.AppContext.BaseDirectory, "ПР45_Осокин.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseMvcWithDefaultRoute();
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Запросы GET");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "Запросы POST");
                c.SwaggerEndpoint("/swagger/v3/swagger.json", "Запросы PUT");
            });
        }
    }
}
