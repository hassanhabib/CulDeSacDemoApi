using System;
using CulDeSacApi.Brokers.Events;
using CulDeSacApi.Brokers.Loggings;
using CulDeSacApi.Brokers.Queues;
using CulDeSacApi.Brokers.Storages;
using CulDeSacApi.Services.Foundations.LibraryAccounts;
using CulDeSacApi.Services.Foundations.LibraryCards;
using CulDeSacApi.Services.Foundations.LocalStudentEvents;
using CulDeSacApi.Services.Foundations.StudentEvents;
using CulDeSacApi.Services.Foundations.Students;
using CulDeSacApi.Services.Orchestrations.LibraryAccounts;
using CulDeSacApi.Services.Orchestrations.StudentEvents;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace CulDeSacApi
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
            services.AddLogging(options =>
            {
                //options.Configure(config =>
                //{
                //    // Adds the SpanId, TraceId, ParentId and
                //    // context information to the logging scope.
                //    config.ActivityTrackingOptions =
                //        ActivityTrackingOptions.SpanId
                //        | ActivityTrackingOptions.TraceId
                //        | ActivityTrackingOptions.ParentId
                //        | ActivityTrackingOptions.Tags
                //        | ActivityTrackingOptions.Baggage;
                //});
                options.AddConsole();
            });

            services.AddControllers();
            services.AddDbContext<StorageBroker>();
            services.AddTransient<IStorageBroker, StorageBroker>();
            services.AddTransient<ILoggingBroker, LoggingBroker>();
            services.AddTransient<IQueueBroker, QueueBroker>();
            services.AddTransient<IEventBroker, EventBroker>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IStudentEventService, StudentEventService>();
            services.AddTransient<ILocalStudentEventService, LocalStudentEventService>();
            services.AddTransient<ILibraryAccountService, LibraryAccountService>();
            services.AddTransient<ILibraryCardService, LibraryCardService>();
            services.AddTransient<IStudentEventOrchestrationService, StudentEventOrchestrationService>();
            services.AddTransient<ILibraryAccountOrchestrationService, LibraryAccountOrchestrationService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CulDeSacApi", Version = "v1" });
            });

            string serviceName = Configuration["ActivitySource"];

            services.AddOpenTelemetryTracing(config => config
                .AddSource(serviceName)
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName).AddTelemetrySdk())
                .AddZipkinExporter(options =>
                {
                    options.Endpoint = new Uri("http://localhost:9411/api/v2/spans");
                })
                .AddJaegerExporter(options =>
                {
                    options.AgentHost = "localhost";
                    options.AgentPort = 6831;
                })
                .AddAspNetCoreInstrumentation(options =>
                {
                    options.Filter = (req) =>
                        !req.Request.Path.ToUriComponent().Contains("index.html", StringComparison.OrdinalIgnoreCase)
                        && !req.Request.Path.ToUriComponent().Contains("swagger", StringComparison.OrdinalIgnoreCase)
                        && !req.Request.Path.ToUriComponent().Contains("applicationinsights.azure.com", StringComparison.OrdinalIgnoreCase);
                }));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CulDeSacApi v1"));
            }

            app.ApplicationServices.GetService<ILibraryAccountOrchestrationService>().ListenToLocalStudentEvent();
            app.ApplicationServices.GetService<IStudentEventOrchestrationService>().ListenToStudentEvents();
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
