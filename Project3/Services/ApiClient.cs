using System.Net.Http.Headers;

namespace Project3.Services;

public class ApiClient
{
    
    private readonly HttpClient _httpClient;
    
    public ApiClient(string baseUrl="")
    {
        _httpClient = new HttpClient(
            new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(1),
                ConnectTimeout = TimeSpan.FromMinutes(1)
            })
        {
            BaseAddress = new Uri(Environment.GetEnvironmentVariable("API_URL") ?? baseUrl),
            Timeout = TimeSpan.FromMinutes(5),
        };
    }

    public async Task<HttpResponseMessage> GetAsync(string url, long start = 0)
    {
        var baseUrl = Environment.GetEnvironmentVariable("API_URL") ?? "";
        
        var request = new HttpRequestMessage(HttpMethod.Get, baseUrl + url);
        if (start > 0)
        {
            request.Headers.Range = new RangeHeaderValue(start, null);
        }
            
        var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
        response.EnsureSuccessStatusCode();

        return response;
    }
}