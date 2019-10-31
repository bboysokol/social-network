using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Generation.Processors.Security;
using SocialNetwork_Backend.Database;
using SocialNetwork_Backend.Helpers;
using SocialNetwork_Backend.Models;
using SocialNetwork_Backend.Providers;
using SocialNetwork_Backend.Responses.Wrappers.Factories;
using SocialNetwork_Backend.Services;
using SocialNetwork_Backend.Services.Interfaces;
using System.Text;

namespace SocialNetwork_Backend
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
            services.AddCors();
            services.AddSwaggerDocument();
            services.AddDbContext<SocialNetworkContext>(
               options => options.UseSqlServer(Configuration.GetConnectionString("SocialNetwork")));

            services.AddScoped<IJwtHelper, JwtHelper>();
            services.AddScoped<IApiResponseFactory, ApiResponseFactory>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddIdentity<User, IdentityRole<int>>()
                .AddEntityFrameworkStores<SocialNetworkContext>();

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);



            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
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

            services.AddSingleton(typeof(IUserIdProvider), typeof(MyUserIdProvider));
            // Add framework services.
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddSwaggerDocument(document =>
            {

                document.Title = "toDo";
                document.DocumentName = "swagger";
                document.OperationProcessors.Add(new OperationSecurityScopeProcessor("jwt"));
                document.DocumentProcessors.Add(new SecurityDefinitionAppender("jwt", new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "JWT Token - remember to add 'Bearer ' before the token",
                }));
            });

            //services.AddSignalR().AddJsonProtocol(options => options.PayloadSerializerSettings.ContractResolver =
            //    new DefaultContractResolver());
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors(builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseCors(builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseOpenApi(options =>
            {
                options.DocumentName = "swagger";
                options.Path = "/swagger/v1/swagger.json";
                options.PostProcess = (document, _) =>
                {
                    document.Schemes.Add(OpenApiSchema.Https);
                };
            });
            app.UseSwaggerUi3(options =>
            {
                options.DocumentPath = "/swagger/v1/swagger.json";
            });

            app.UseExceptionHandler(new ExceptionHandlerOptions()
            {
                ExceptionHandler = new Middleware.JsonExceptionMiddleware().Invoke
            });

            app.UseAuthentication();
            //app.UseSignalR(routes =>
            //{
            //    routes.MapHub<NotificationHub>("/notification");
            //});
        }
    }
}
