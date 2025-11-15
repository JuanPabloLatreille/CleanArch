using Application.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection;

public static class ValidationPipelineInjection
{
    public static IServiceCollection AddValidationPipeline(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(
            typeof(ValidationPipelineInjection).Assembly,
            includeInternalTypes: false);
        
        services.AddTransient(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationPipelineBehavior<,>));

        return services;
    }
}