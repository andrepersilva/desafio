using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using ZXVentures.Domain;
using ZXVentures.Domain.Entities;
using ZXVentures.Domain.Interfaces;
using ZXVentures.Infra.Data;
using ZXVentures.Service;

namespace ZXVentures.Aplication
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

            services.Configure< PdvDataSettings>(
                Configuration.GetSection(nameof(PdvDataSettings)));

            services.AddSingleton<IPdvDataSettings>(sp =>
                sp.GetRequiredService<IOptions<PdvDataSettings>>().Value);

            services.AddSingleton<IRepositoryPdv, RepositoryPdv>();
            services.AddSingleton<IServicePdv,ServicePdv>();

            services.AddControllers(option=> option.EnableEndpointRouting=false).AddNewtonsoftJson(options => options.UseCamelCasing(true)); ;


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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
