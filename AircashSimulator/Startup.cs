using DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Services.AbonSalePartner;
using Services.AbonOnlinePartner;
using Services.HttpRequest;
using AircashSimulator.Configuration;

namespace AircashSimulator
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            
            services.AddDbContext<AircashSimulatorContext>(options => options.UseSqlServer(Configuration["AbonSimulatorConfiguration:ConnectionString"]), ServiceLifetime.Transient);
            services.AddHttpClient<IHttpRequestService, HttpRequestService>();

            services.AddTransient<IAbonSalePartnerService, AbonSalePartnerService>();
            services.AddTransient<IAbonOnlinePartnerService, AbonOnlinePartnerService>();
            services.AddTransient<IHttpRequestService, HttpRequestService>();

            services.Configure<AbonConfiguration>(Configuration.GetSection("AbonConfiguration"));
        }

        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseCors();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
