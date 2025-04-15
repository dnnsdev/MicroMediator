namespace MicroMediator;

/// <summary>
/// Represents the request handler
/// </summary>
/// <typeparam name="TRequest">Type of request</typeparam>
/// <typeparam name="TResponse">Type of response</typeparam>
/// <remarks>Has a constraint for TRequest</remarks>
public interface IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    /// <summary>
    /// Handle the request
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response of type TResponse</returns>
    Task<TResponse> Handle(IRequest<TResponse> request, CancellationToken cancellationToken);
}