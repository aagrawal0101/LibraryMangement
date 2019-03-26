using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LibraryManagement.BusinessLayers;
using LibraryManagement.Repository;
using LibraryManager.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace LibraryManagementSystem
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
            services.AddAutoMapper();
            services.AddMvc();
            services.AddEntityFrameworkSqlServer();
            services.AddDbContext<LibraryDbContext>((options) =>
            {
                options.UseSqlServer("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=BookDB;Data Source=XIPLO9260");
            });

            services.AddScoped<ILibraryManager, LibraryManagers>();
            services.AddScoped<ILibraryRepository, LibraryRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();

            ConfigureMappers();
        }

        private void ConfigureMappers()
        {
            LibraryManagement.Mappers.DtoDomainMapper.ConfigMapper();
        }
    }
}
