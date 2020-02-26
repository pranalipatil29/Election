using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectionBusinessLayer.InterfaceBL;
using ElectionBusinessLayer.ServiceBL;
using ElectionCommonLayer.Model;
using ElectionRepositoryLayer.Context;
using ElectionRepositoryLayer.InterfaceRL;
using ElectionRepositoryLayer.ServiceRL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace ElectionApplication
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
            services.AddTransient<IAdminBL, AdminBL>();
            services.AddTransient<IAdminRL, AdminRL>();

            services.AddTransient<IPartiesBL, PartiesBL>();
            services.AddTransient<IPartiesRL, PartiesRL>();

            services.AddTransient<IUserBL, UserBL>();
            services.AddTransient<IUserRL, UserRL>();

            services.AddTransient<ICandidateBL, CandidateBL>();
            services.AddTransient<ICandidateRL, CandidateRL>();

            services.AddTransient<IConstituencyBL, ConstituencyBL>();
            services.AddTransient<IConstituencyRL, ConstituencyRL>();

            services.AddTransient<IStateBL, StateBL>();
            services.AddTransient<IStateRL, StateRL>();

            services.Configure<ApplicationSetting>(this.Configuration.GetSection("ApplicationSetting"));

            services.AddDbContext<AuthenticationContext>(Options =>
            Options.UseSqlServer(this.Configuration.GetConnectionString("IdentityConnection")));

            services.AddDefaultIdentity<ApplicationModel>().AddEntityFrameworkStores <AuthenticationContext>();


            var key = Encoding.UTF8.GetBytes(this.Configuration["ApplicationSetting:JWTSecret"].ToString());

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod().AllowAnyHeader());
            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;

                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Election API", Description = "Swagger Core API" });

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "Swagger",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                    {
                        "Bearer", new string[] { } }
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Election API");
            });

            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
