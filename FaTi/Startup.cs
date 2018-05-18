using System.IO;
using Akka.Actor;
using BVirtual.FaTi.Common;
using BVirtual.FaTi.Common.Config;
using BVirtual.FaTi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace BVirtual.FaTi
{
    public class Startup
    {
        private const string HoconFileName = "akka.config";

        public Startup(IHostingEnvironment env)
        {
            
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                    .AddApiExplorer()
                    .AddJsonFormatters(delegate (JsonSerializerSettings settings) {
                        settings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                        settings.Error = (sender, args) =>
                        {
                            var logger = LoggerFactoryManager.GetLogger<JsonSerializerSettings>();
                            var objectName = args.CurrentObject?.GetType().FullName ?? "Unknown";
                            logger.LogWarning($"Failed to deserialize [{objectName}] JSON on input: {args.ErrorContext.Error.Message}");
                        };
                    });

            // Add the config
            services.AddSingleton(Program.AppConfig);
            // Create the actor system
            services.AddSingleton(_ =>
            {
                // Simple solution to inject our logger
                LoggerFactoryManager.Init(_);

                return ActorSystem.Create("faresandticketing", HoconLoader.FromFile(Path.Combine(Directory.GetCurrentDirectory(), HoconFileName)));
            });

            // Add the ES client implementation
            //services.AddSingleton<IESClient, ESClient>(_ => new ESClient(_.GetRequiredService<ILogger<ESClient>>(), _.GetRequiredService<AppConfig>()));

            services.AddFareServices();
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Info {Title = "Quote Validator V1", Version = "v1"}));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fare and Ticketing - FATI V1");
            });

            //// Get the ProductManagerActorProvider to pre-heat the products from TariffManagement
            //var pmaProvider = (ProductManagerActorProvider)app.ApplicationServices.GetService(typeof(ProductManagerActorProvider));
            //pmaProvider.Get();
        }
    }
}
