using Ecommerce.BussinessLayer;
using Ecommerce.BussinessLayer.LoginManagement;
using Ecommerce.BussinessLayer.UserManagment;
using Ecommerce.DataAccess;
using Ecommerce.Tools;
using Ecommerce.Tools.ExtensionsMethods;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.Swagger;
using System;
using System.Text;

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
            services.AddSingleton<DefaultValues>();
            services.AddScoped<IUserManager,UserManager>();
            services.AddScoped<ILoginManager,LoginManager>();
            services.AddServiceContext(Configuration);
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;

            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddNewtonsoftJson();

            services.AddCors(options => 
                    options.AddPolicy("ecmm_rules",
                                       builder => builder.SetIsOriginAllowed(_ => true)
                                                         .AllowAnyMethod()
                                                         .AllowAnyHeader()
                                                         .AllowCredentials()                                                         
                                       ));

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuer = true,
            //            ValidateIssuerSigningKey = true,
            //            ValidateAudience = true,
            //            ValidateLifetime = true,
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["key_secret"])),
            //            ClockSkew = TimeSpan.Zero,
            //            ValidIssuer = "SophianCustomer",
            //            ValidAudience = "SophianEcommerce"
            //        }
            //    );

            services.AddMvc().AddNewtonsoftJson(opt => 
                            opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddLogging(config =>
            {
                config.AddAWSProvider(Configuration.GetAWSLoggingConfigSection());
                config.SetMinimumLevel(LogLevel.Debug);
            });

            //services.AddRedis();

            //services.AddSwaggerGen(
            ////    sgo =>
            ////{
            ////    sgo.SwaggerDoc("v1", new Info { title = "SophianEcommerce API", version = "v1" });
            ////}
            //);
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

            app.UseCors("ecmm_rules");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
