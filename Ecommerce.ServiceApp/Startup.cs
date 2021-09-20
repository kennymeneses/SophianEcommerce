using Ecommerce.BussinessLayer.UserManagment;
using Ecommerce.DataAccess;
using Ecommerce.Tools;
using Ecommerce.Tools.ExtensionsMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Ecommerce.ServiceApp
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
            services.AddScoped<DataContext>();
            services.AddSingleton<EncryptData>();
            services.AddScoped<IUserManager,UserManager>();
            services.AddServiceContext(Configuration);
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;

            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddNewtonsoftJson();

            services.AddMvc().AddNewtonsoftJson(opt => 
                            opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
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
