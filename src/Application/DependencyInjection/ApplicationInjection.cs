using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection;

public static class ApplicationInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(ApplicationInjection).Assembly);
        });

        services.AddValidationPipeline();

        return services;
    }
}