using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoronaApp.Dal;
using CoronaApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore;
using CoronaApp.Api.Middleware;
using Glimpse.Mvc.AlternateType;
using CoronaApp.Services.Helpers;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Reflection;
using System.IO;

namespace CoronaApp.Api
{

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
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
            services.AddDbContext<CoronaContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("CoronaContext")));
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IPatientService, PatientService>();
            //services.AddMvc(options =>
            //{
            //    options.Filters.Add(new AuthorizationFilter());
            //    options.Filters.Add(new ErrorHandlingFilter());
            //});

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            // services.AddCors();
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("CoroanAPI",new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                Description="CoronaAPI add swagger",
                Title="Corona",
                Version="1"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                setupAction.IncludeXmlComments(xmlPath);
               
            });

            

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler();
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/CoroanAPI/swagger.json", "Corona");
            });


            app.UseMiddleware<ValedationMiddleware>();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());



            app.UseAuthentication();
            app.UseAuthorization();


            app.UseStaticFiles();

            app.UseAuthorization();
            //app.UseExceptionHandler();

            //app.UseNodeModules(env);

            //app.UseMvc();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
              //  endpoints.MapRazorPages();
            });
            //  app.UseStatusCodePagesWithReExecute("/Errors/{0}"); 

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always
            });


        }


    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member