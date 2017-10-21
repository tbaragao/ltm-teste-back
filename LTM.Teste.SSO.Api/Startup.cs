using Common.Domain.Base;
using Common.Domain.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using IdentityServer4;

namespace Sso.Server.Api
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        private readonly IHostingEnvironment _env;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(env.ContentRootPath)
                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                 .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                 .AddEnvironmentVariables();

            Configuration = builder.Build();
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddTemporarySigningCredential()
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryClients(Config.GetClients(Configuration.GetSection("ConfigSettings").Get<ConfigSettingsBase>()));
            
            services.AddScoped<CurrentUser>();
            services.Configure<ConfigSettingsBase>(Configuration.GetSection("ConfigSettings"));
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddCors(options =>
            {
                options.AddPolicy("frontcore", policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });

                options.AddPolicy("backcore", policy =>
                {
                    policy.WithOrigins("http://localhost:8122")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IOptions<ConfigSettingsBase> configSettingsBase)
        {
            loggerFactory.AddConsole(LogLevel.Debug);
            app.UseDeveloperExceptionPage();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddFile("Logs/sm-sso-server-api-{Date}.log");

            app.UseCors("frontcore");
            app.UseCors("backcore");

            app.UseIdentityServer();
            app.UseGoogleAuthentication(new GoogleOptions
            {
                AuthenticationScheme = "Google",
                SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
                ClientId = "152985192296-pull366pkttdg3r310kphpr6n0ia7ugr.apps.googleusercontent.com",
                ClientSecret = "K4KAe33w0qYfBSIf0SIDe0jh"
            });

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }

    }
}
