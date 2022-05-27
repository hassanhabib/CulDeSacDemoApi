using CulDeSacApi.Brokers.Events;
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
using Microsoft.OpenApi.Models;

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
            services.AddControllers();
            services.AddDbContext<StorageBroker>();
            services.AddTransient<IStorageBroker, StorageBroker>();
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
