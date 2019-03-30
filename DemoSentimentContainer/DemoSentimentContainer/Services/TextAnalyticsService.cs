using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;

using DemoSentimentContainer.Models;
using DemoSentimentContainer.Helpers;

using Newtonsoft.Json;

namespace DemoSentimentContainer.Services
{
    public static class TextAnalyticsService
    {
        private static readonly HttpClient client = CreateHttpClient();

        public async static Task<Document> AnalyzeText(string message, string language)
        {
            try
            {
                var documents = PrepareRequest(message, language);

                var content = new StringContent(documents);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PostAsync("text/analytics/v2.0/sentiment", content);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<DocumentResponse>(json);
                    return result.documents.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
            }

            return default(Document);
        }

        private static HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Constants.LocalEndpoint);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        }

        private static string PrepareRequest(string message, string language)
        {
            var wrapper = new
            {
                documents = new List<DocumentRequest>()
                {
                    new DocumentRequest()
                    {
                        Id = "1",
                        Language = language,
                        Text = message
                    }
                }
            };

            return JsonConvert.SerializeObject(wrapper);
        }
    }
}
