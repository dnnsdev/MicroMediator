using Microsoft.Extensions.DependencyInjection;

namespace MicroMediator;

/// <summary>
/// Sends (or mediates) the object.
/// </summary>
/// <param name="serviceProvider">Service provider</param>
public class MediatorSender(IServiceProvider serviceProvider) : ISender
{
    public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        // Determine the type of the handler
        Type typeOfHandler = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        
        // Since the handler can be of almost any type, dynamic is used.
        // The handler needs to be fetched from the service provider.
        dynamic concreteHandler = serviceProvider.GetRequiredService(typeOfHandler);

        // Handle request accordingly
        return concreteHandler.Handle((dynamic)request, cancellationToken);
    }
}