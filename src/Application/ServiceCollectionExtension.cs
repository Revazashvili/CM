using System.Reflection;
using Application.Common.Behaviours;
using FluentValidation;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    /// <summary>
    /// Extension class for <see cref="IServiceCollection"/> interface
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Injects application dependencies into dependency injection container.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> interface.</param>
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMapper();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TaskCanceledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
        }

        /// <summary>
        /// Configures mapper settings and injects services into dependency injection container.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> interface.</param>
        private static void AddMapper(this IServiceCollection services)
        {
            var typeAdapterConfig = new TypeAdapterConfig();
            typeAdapterConfig.EnableImmutableMapping();
            typeAdapterConfig.EnableJsonMapping();
            services.AddSingleton(typeAdapterConfig);
            services.AddScoped<IMapper, ServiceMapper>();
        }
    }
}