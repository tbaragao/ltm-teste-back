using Common.API;
using Common.Domain.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using LTM.Teste.Application.Config;
using LTM.Teste.Data.Context;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Common.API.Extensions;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;

namespace LTM.Teste.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                });

            services
                .AddDbContext<DbContextCore>(options => options.UseSqlServer(Configuration.GetSection("EFCoreConnStrings:Core").Value));

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = Configuration.GetSection("RedisConnStrings:Core").Value;
                options.InstanceName = "Core";
            });

            services.Configure<ConfigSettingsBase>(Configuration.GetSection("ConfigSettings"));
            services.AddSingleton<IConfiguration>(Configuration);

            Cors.Enable(services);

            ConfigContainerCore.Config(services);

            services
                .AddMvc(options => { options.ModelBinderProviders.Insert(0, new DateTimePtBrModelBinderProvider()); })
                .AddJsonOptions(options => { options.SerializerSettings.Converters.Add(new DateTimePtBrConverter()); });

            services
                .AddSwaggerGen(c => 
                {
                    c.SwaggerDoc("v1", new Info { Title = "LTM Teste Api", Version = "v1" });

                    c.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                    {
                        Description = "Acesse o painel administrativo onde realizou o login e clique para copiar o código exibido logo abaixo do seu nome.",
                        Name = "Authorization",
                        In = "header",
                        Type = "apiKey"
                    });

                });

        }
        

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IOptions<ConfigSettingsBase> configSettingsBase)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseDeveloperExceptionPage();
            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            {
                Authority = configSettingsBase.Value.AuthorityEndPoint,
                ApiName = "ltmteste-spa",
                
                RequireHttpsMetadata = false,
            });

            var supportedCultures = new[] { new CultureInfo("pt-BR") };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LTM Teste Api v1");
            });

            app.AddTokenMiddleware();
            app.UseMvc();
            app.UseCors("AllowAnyOrigin");
            AutoMapperConfigCore.RegisterMappings();
        }

    }
}
