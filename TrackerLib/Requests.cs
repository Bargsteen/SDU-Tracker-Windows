using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TrackerLib
{
  public class Requests
  {
    private static readonly HttpClient client = new HttpClient(new LoggingHandler(new HttpClientHandler()));
    private const string baseUrl = "https://screens.sdu.dk/v1/";
    public static void SendUsageAsync(Usage usage, Credentials credentials)
    {
      // Set url dependent on usage type
      string urlEnding = "";
      switch(usage)
      {
        case AppUsage _:
          urlEnding = "app_usages";
          break;
        case DeviceUsage _:
          urlEnding = "device_usages";
          break;
      }
      var url = baseUrl + urlEnding;

      // Content
      var jsonUsage = usage.ToJson();
      var content = new StringContent(jsonUsage, Encoding.UTF8, "application/json");

      // Authorization
      string encodedCredentials = System.Convert
                              .ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                              .GetBytes($"{credentials.Username}:{credentials.Password}"));

      client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", encodedCredentials);

      // Request
      var response = client.PostAsync(url, content).Result;
    }
  }

  public class Credentials
  {
    public string Username { get; }
    public string Password { get; }

    public Credentials(string username, string password)
    {
      Username = username;
      Password = password;
    }
  }

  public class LoggingHandler : DelegatingHandler
  {
    public LoggingHandler(HttpMessageHandler innerHandler)
        : base(innerHandler)
    {
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
      Console.WriteLine("Request:");
      Console.WriteLine(request.ToString());
      if (request.Content != null)
      {
        Console.WriteLine(await request.Content.ReadAsStringAsync());
      }
      Console.WriteLine();

      HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

      Console.WriteLine("Response:");
      Console.WriteLine(response.ToString());
      if (response.Content != null)
      {
        Console.WriteLine(await response.Content.ReadAsStringAsync());
      }
      Console.WriteLine();

      return response;
    }
  }
}