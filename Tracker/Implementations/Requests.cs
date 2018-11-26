using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Tracker.Interfaces;
using Tracker.Models;

namespace Tracker.Implementations
{
    public class Requests : IRequests
    {
        private static readonly HttpClient Client = new HttpClient();
        private const string BaseUrl = "https://screens.sdu.dk/v1/";
        public void SendUsageAsync(Usage usage, Credentials credentials, Action onSuccess, Action onError)
        {
            // Set url dependent on usage type
            string urlEnding = "";
            switch (usage)
            {
                case AppUsage _:
                    urlEnding = "app_usages";
                    break;
                case DeviceUsage _:
                    urlEnding = "device_usages";
                    break;
            }
            string url = BaseUrl + urlEnding;

            // Content
            string jsonUsage = usage.ToJson();
            var content = new StringContent(jsonUsage, Encoding.UTF8, "application/json");

            // Authorization
            string encodedCredentials = Convert
                                    .ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                                    .GetBytes($"{credentials.Username}:{credentials.Password}"));

            Client.DefaultRequestHeaders.Authorization =
              new AuthenticationHeaderValue("Basic", encodedCredentials);


            // Request
            HttpResponseMessage response = null;
            try
            {
                response = Client.PostAsync(url, content).Result;
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch { } // We are not interested in the error. Just call onError if anything went wrong.
            finally
            {
                if (response?.StatusCode == HttpStatusCode.OK)
                {
                    onSuccess();
                }
                else
                {
                    onError();
                }
            }
        }
    }
}