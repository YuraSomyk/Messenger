using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Messenger.BLL.Services.Message.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Messenger.BLL.Services.Message;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Messenger.DAL.Interface;
using Messenger.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Messenger.BLL.Services.User.Interface;
using Messenger.BLL.Services.User;
using Messenger.BLL.Services.Jwt.Interface;
using Messenger.BLL.Services.Jwt;

namespace Messenger {

    public class Startup {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) {

            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            services.AddCors(options => {
                options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials().Build());
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

            services.AddSpaStaticFiles(configuration => {
                configuration.RootPath = "ClientApp/build";
            });

            services.AddControllersWithViews();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IMessageService, MessageService>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IJwtService, JwtService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Error");

                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            app.UseSpa(spa => {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment()) {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}