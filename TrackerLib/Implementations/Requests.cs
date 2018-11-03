using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using TrackerLib.Interfaces;
using TrackerLib.Models;

namespace TrackerLib.Implementations
{
    public class Requests : IRequests
    {
        private static readonly HttpClient client = new HttpClient();
        private const string baseUrl = "https://screens.sdu.dk/v1/";
        public void SendUsageAsync(IUsage usage, Credentials credentials, Action onSuccess, Action onError)
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
            var url = baseUrl + urlEnding;

            // Content
            var jsonUsage = usage.ToJson();
            var content = new StringContent(jsonUsage, Encoding.UTF8, "application/json");

            // Authorization
            string encodedCredentials = Convert
                                    .ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                                    .GetBytes($"{credentials.Username}:{credentials.Password}"));

            client.DefaultRequestHeaders.Authorization =
              new AuthenticationHeaderValue("Basic", encodedCredentials);


            // Request
            HttpResponseMessage response = null;
            try
            {
                response = client.PostAsync(url, content).Result;
            }
            catch { } // Not really interested in the Error. Just needed to avoid runtime errors.
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