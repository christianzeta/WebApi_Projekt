using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApi_Projekt.Data;
using WebApi_Projekt.Models;

namespace WebApi_Projekt
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
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
            services.AddVersionedApiExplorer(o =>
            {
                o.GroupNameFormat = "'v'VVV";
                o.SubstituteApiVersionInUrl = true;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi_Projekt", Version = "v1.0" });
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "WebApi_Projekt", Version = "v2.0" });
                var path = Path.Combine(AppContext.BaseDirectory, "Documentation.xml");
                c.IncludeXmlComments(path);
            });
            services.AddDbContext<Context>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DbConn")));

            services.AddDefaultIdentity<MyUser>(options =>
            options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<Context>();

            services.AddAuthentication("MyAuthScheme")
                .AddScheme<AuthenticationSchemeOptions, AuthenticationHandler>("MyAuthScheme", null);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi_Projekt v1.0");
                    c.SwaggerEndpoint("/swagger/v2/swagger.json", "WebApi_Projekt v2.0");
                });
            }
                app.UseHttpsRedirection();

                app.UseRouting();

                app.UseAuthentication();

                app.UseAuthorization();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            }
        }
}
