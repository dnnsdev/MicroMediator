namespace MicroMediator;

/// <summary>
/// Sender interface.
/// </summary>
public interface ISender
{
    /// <summary>
    /// Send object to handler
    /// </summary>
    /// <param name="request">Request object</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <typeparam name="TResponse">Response of type TResponse</typeparam>
    /// <returns>Value of TResponse</returns>
    Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
}