using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TrackYourPresence.Services
{
    public abstract class AbstractServiceBase
    {
        protected HttpClient GetHttpClient()
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
                (message, certificate, chain, sslPolicyErrors) => true;

            return new HttpClient(httpClientHandler);
        }

        protected Task<HttpResponseMessage> HttpGet(string uri)
        {
            return HttpGet(new Uri(uri));
        }

        protected Task<HttpResponseMessage> HttpGet(Uri uri)
        {
            var client = GetHttpClient();
            return client.GetAsync(uri);
        }

        protected Task<HttpResponseMessage> HttpPost<T>(string uri, T obj)
        {
            return HttpPost(new Uri(uri), obj);
        }

        protected Task<HttpResponseMessage> HttpPost<T>(Uri uri, T obj)
        {
            var client = GetHttpClient();
            var json = JsonConvert.SerializeObject(obj);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return client.PostAsync(uri, content);
        }

        protected Task<HttpResponseMessage> HttpPut<T>(string uri, T obj)
        {
            return HttpPut(new Uri(uri), obj);
        }

        protected Task<HttpResponseMessage> HttpPut<T>(Uri uri, T obj)
        {
            var client = GetHttpClient();
            var json = JsonConvert.SerializeObject(obj);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return client.PutAsync(uri, content);
        }
    }
}