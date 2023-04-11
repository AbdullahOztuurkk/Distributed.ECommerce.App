using Clicco.Infrastructure.Helpers.Contracts;
using System.Text;
using System.Text.Json;

public class HttpRestClientHelper : IHttpClientHelper
{
    private readonly string _baseUrl;
    public HttpRestClientHelper(string baseUrl)
    {
        _baseUrl = baseUrl;
    }

    public async Task<TResponse> GetAsync<TResponse>(string endpoint, IDictionary<string, string> headers = null)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, _baseUrl + endpoint);
        AddHeaders(request, headers);

        var response = await client.SendAsync(request);
        return await Deserialize<TResponse>(response);
    }

    public async Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest body, IDictionary<string, string> headers = null)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, _baseUrl + endpoint);
        AddHeaders(request, headers);

        request.Content = await SerializeStringContent<TRequest>(body);

        var response = await client.SendAsync(request);
        return await Deserialize<TResponse>(response);
    }

    public async Task<TResponse> PutAsync<TRequest, TResponse>(string endpoint, TRequest body, IDictionary<string, string> headers = null)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Put, _baseUrl + endpoint);
        AddHeaders(request, headers);

        request.Content = await SerializeStringContent<TRequest>(body);

        var response = await client.SendAsync(request);
        return await Deserialize<TResponse>(response);
    }

    public async Task<TResponse> DeleteAsync<TResponse>(string endpoint, IDictionary<string, string> headers = null)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Delete, _baseUrl + endpoint);
        AddHeaders(request, headers);

        var response = await client.SendAsync(request);
        return await Deserialize<TResponse>(response);
    }

    private void AddHeaders(HttpRequestMessage request, IDictionary<string, string> headers)
    {
        if (headers != null)
        {
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
        }
    }

    private async Task<TResponse> Deserialize<TResponse>(HttpResponseMessage responseMessage)
    {
        var content = await responseMessage.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<TResponse>(content);
    }

    private async Task<StringContent> SerializeStringContent<TRequest>(TRequest body)
    {
        var jsonBody = JsonSerializer.Serialize(body);
        return new StringContent(jsonBody,Encoding.UTF8, "application/json");
    }
}
