#nullable enable
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TrackYourPresence.Services
{
    public abstract class AbstractServiceBase<T>
    {
        protected HttpClient GetHttpClient()
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
                (message, certificate, chain, sslPolicyErrors) => true;

            return new HttpClient(httpClientHandler);
        }

        protected Task<HttpResponseMessage> HttpGet(string uri, T? obj, Guid? guid)
        {
            return HttpGet(new Uri(uri), obj, guid);
        }

        protected Task<HttpResponseMessage> HttpGet(Uri uri, T? obj, Guid? guid)
        {
            return HttpGet(uri, ToDataObject(obj, guid));
        }

        protected Task<HttpResponseMessage> HttpGet(Uri uri, Data<T> data)
        {
            var client = GetHttpClient();
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = uri,
                Content = content
            };
            return client.SendAsync(request);
        }

        protected Task<HttpResponseMessage> HttpPost(string uri, T? obj, Guid? guid)
        {
            return HttpPost(new Uri(uri), obj, guid);
        }

        protected Task<HttpResponseMessage> HttpPost(Uri uri, T? obj, Guid? guid)
        {
            return HttpPost(uri, ToDataObject(obj, guid));
        }

        protected Task<HttpResponseMessage> HttpPost(Uri uri, Data<T> data)
        {
            var client = GetHttpClient();
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return client.PostAsync(uri, content);
        }

        protected Task<HttpResponseMessage> HttpPut(string uri, T? obj, Guid? guid)
        {
            return HttpPut(new Uri(uri), obj, guid);
        }

        protected Task<HttpResponseMessage> HttpPut(Uri uri, T? obj, Guid? guid)
        {
            return HttpPut(uri, ToDataObject(obj, guid));
        }

        protected Task<HttpResponseMessage> HttpPut(Uri uri, Data<T> data)
        {
            var client = GetHttpClient();
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return client.PutAsync(uri, content);
        }

        protected static Data<T> ToDataObject(T? obj, Guid? guid)
        {
            return new()
            {
                DeviceId = App.DeviceId,
                Uuid = guid,
                Entity = obj,
            };
        }

        protected static TO FromJson<TO>(string json)
        {
            return JsonConvert.DeserializeObject<TO>(json) ?? throw new InvalidOperationException();
        }
    }
}