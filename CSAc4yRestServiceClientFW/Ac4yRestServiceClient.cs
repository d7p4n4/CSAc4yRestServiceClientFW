using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace SycomplaWebAppClientCore
{
    public class Ac4yRestServiceClient
    {
        public string URL { get; set; }

        public Ac4yRestServiceClient() { }

        public Ac4yRestServiceClient(string url)
        {
            URL = url;
        }

        public string GET(string path, string DATA)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(URL + path);
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpRequestMessage request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(URL),
                Content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json"),
            };

            HttpContent content = new StringContent(DATA, UTF8Encoding.UTF8, "application/json");

            HttpResponseMessage message = httpClient.SendAsync(request).Result;
            string result = message.Content.ReadAsStringAsync().Result;

            return result;
        }

        public string GET(string pathAndParameter)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(URL + "?" + pathAndParameter);
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            
            HttpResponseMessage message = httpClient.GetAsync(URL).Result;
            string result = message.Content.ReadAsStringAsync().Result;

            return result;
        }

        public string POST(string path, string DATA)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), URL + path))
                {
                    request.Content = new StringContent(DATA);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = httpClient.SendAsync(request).Result;
                    return response.Content.ReadAsStringAsync().Result;
                }
            }
        }

    }
}
