using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace MicroMediator.Infrastructure;

/// <summary>
/// Startup extensions for MicroMediator
/// </summary>
public static class StartupExtensions
{
    /// <summary>
    /// Add MicroMediator to project and setup all required services.
    /// </summary>
    /// <param name="serviceCollection">Service collection</param>
    /// <param name="assembly">Optional other relevant assembly</param>
    /// <returns>Service collection</returns>
    public static IServiceCollection AddMicroMediator(this IServiceCollection serviceCollection, Assembly? assembly)
    {
        // Set the assembly
        assembly ??= Assembly.GetCallingAssembly();
        
        // Add initial required scoped service
        serviceCollection.AddScoped<ISender, Sender>();

        // Determine type of handler interface
        Type typeOfHandlerInterface = typeof(IRequestHandler<,>);
        
        // So that we are able to find all concrete implementations of this interface and register them, through reflection.
        var concreteImplementations = assembly
            .GetTypes()
            .Where(type => type is { IsInterface: false, IsAbstract: false })
            .SelectMany(type => type.GetInterfaces()
                .Where(mediatorInterface => mediatorInterface.IsGenericType
                                            && mediatorInterface.GetGenericTypeDefinition() == typeOfHandlerInterface)
                .Select(concreteImplementation => new
                {
                    Interface = concreteImplementation,
                    MediatorImplementation = type
                })
            );

        // Finally, add all implementations as scoped to services collection so that manually registering them is not needed.
        foreach (var concreteImplementation in concreteImplementations)
            serviceCollection.AddScoped(concreteImplementation.Interface, concreteImplementation.MediatorImplementation);
        
        return serviceCollection;
    }
}