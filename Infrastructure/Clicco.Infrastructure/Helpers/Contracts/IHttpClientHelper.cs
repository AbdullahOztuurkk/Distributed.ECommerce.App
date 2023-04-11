namespace Clicco.Infrastructure.Helpers.Contracts
{
    public interface IHttpClientHelper
    {
        Task<TResponse> GetAsync<TResponse>(string endpoint, IDictionary<string, string> headers = null);

        Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest body, IDictionary<string, string> headers = null);

        Task<TResponse> PutAsync<TRequest, TResponse>(string endpoint, TRequest body, IDictionary<string, string> headers = null);

        Task<TResponse> DeleteAsync<TResponse>(string endpoint, IDictionary<string, string> headers = null);
    }
}