using System.Diagnostics.CodeAnalysis;
using System.Net;
using Microsoft.AspNetCore.WebUtilities;
using Project3.Constants;
using Project3.Services;

namespace TestProject1;

public class ApiClientTests
{ 
    [Fact]
    public async Task ApiClient_GetAsync_Schedule()
    {
       // Declare
       const string token = "reeEQitM0rEsVOdhd7Ed";
       const string baseUrl = "http://146.190.130.247:5011";
       var endpoint = DefaultEndpoints.Schedule;
       endpoint = QueryHelpers.AddQueryString(endpoint, "token", token);
       var apiClient = new ApiClient(baseUrl);
       
       // Act
       var response = await apiClient.GetAsync(endpoint);
       var content = await response.Content.ReadAsStreamAsync();
       
       // Assert
       Assert.Equal(HttpStatusCode.OK, response.StatusCode);
       Assert.NotNull(content);
    }
    
    [Fact]
    public async Task ApiClient_GetAsync_ScheduleExpanded()
    {
        // Declare
        const string token = "reeEQitM0rEsVOdhd7Ed";
        const string baseUrl = "http://146.190.130.247:5011";
        var endpoint = DefaultEndpoints.ScheduleExpanded;
        endpoint = QueryHelpers.AddQueryString(endpoint, "token", token);
        var apiClient = new ApiClient(baseUrl);
       
        // Act
        var response = await apiClient.GetAsync(endpoint);
        var content = await response.Content.ReadAsStreamAsync();
       
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(content);
    }
}