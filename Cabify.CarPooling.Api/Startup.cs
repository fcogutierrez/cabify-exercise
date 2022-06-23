using Cabify.CarPooling.Application.Commands;
using Cabify.CarPooling.Application.Dtos;
using Cabify.CarPooling.Application.Handlers.CommandHandlers;
using Cabify.CarPooling.Application.Handlers.Decorators;
using Cabify.CarPooling.Application.Handlers.QueryHandlers;
using Cabify.CarPooling.Application.Interfaces;
using Cabify.CarPooling.Application.Query;
using Cabify.CarPooling.Infra.Common.Configuration;
using Cabify.CarPooling.Infra.Persistence.EntityFramework.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cabify.CarPooling.Api
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
            services.AddSwaggerGen();
            services.AddHealthChecking();
            services.AddEntityFrameworkPersistence();

            services.AddScoped<ICommandHandler<AddJourneyCommand>, AddJourneyCommandHandler>();
            services.AddScoped<ICommandHandler<AddCarsCommand>, AddCarsCommandHandler>();
            services.AddScoped<ICommandHandler<DropOffJourneyCommand>, DropOffJourneyCommandHandler>();
            services.AddScoped<IQueryHandler<LocateJourneyCarQuery, CarDto>, LocateJourneyCarQueryHandler>();

            services.Decorate(typeof(ICommandHandler<>), typeof(CommandHandlerLoggerDecorator<>));
            services.Decorate(typeof(IQueryHandler<,>), typeof(QueryHandlerLoggerDecorator<,>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                opt.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
