using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using Project3.Constants;
using Project3.Services;

Console.WriteLine("Hello, select a number to make an API call" +
                  "\nOption (1): /v2/schedule" +
                  "\nOption (2): /v2/schedule_expanded");

var failure = false;
var option = 0;

do
{
    if (failure)
    {
        Console.WriteLine("Incorrect number, these are the available choices: [1, 2]");
    }

    if (!int.TryParse(Console.ReadLine(), out option) || (option != 1 && option != 2))
    {
        failure = true;
        continue;
    }
    
    failure = false;

} while (failure);

var selectedEndpoint = option == 1 
    ? DefaultEndpoints.Schedule 
    : DefaultEndpoints.ScheduleExpanded;

selectedEndpoint = QueryHelpers.AddQueryString(
    selectedEndpoint,
    "token",
    Environment.GetEnvironmentVariable("TOKEN") ?? "");

long downloadedBytes = 0;
var isRetry = false;
var apiClient = new ApiClient();

do
{
    try
    {
        isRetry = false;
        using var response = await apiClient.GetAsync(selectedEndpoint, downloadedBytes);

        await using var stream = await response.Content.ReadAsStreamAsync();
        var buffer = new byte[2048];
        int bytesRead;

        while ((bytesRead = await stream.ReadAsync(buffer)) > 0)
        {
            Console.Write(Encoding.UTF8.GetString(buffer, 0, bytesRead));
            downloadedBytes += bytesRead;
        }
    }
    catch (IOException)
    {
        isRetry = true;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Something went wrong with the API call\n{ex.Message}");
        break;
    }
    
} while (isRetry);