using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.Services
{
    class APIService
    {
        //https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client

        private String route = "http://fncs.eu/api/forward/";
        private HttpClient httpClient = new HttpClient();

        public APIService()
        {
            httpClient.BaseAddress = new Uri(route);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<HttpResponseMessage> Post(string jsonObj, string requestUri)
        {
            string content = "{\"data\":" + jsonObj + "}";
            return await CommonPost(content, requestUri);
        }

        /// <summary>
        /// Post to API
        /// </summary>
        /// <param name="obj">Object in body</param>
        /// <param name="requestUri">Path of api</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> Post(object obj, string requestUri)
        {
            string content = "{\"data\":" + JsonConvert.SerializeObject(obj) + "}";
            return await CommonPost(content, requestUri);
        }

        private async Task<HttpResponseMessage> CommonPost(string content, string requestUri)
        {
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage result = await httpClient.PostAsync(requestUri, byteContent);
            //result.EnsureSuccessStatusCode();
            return result;
        }

        public async Task<HttpResponseMessage> Get(string requestUri)
        {
            
            HttpResponseMessage result = await httpClient.GetAsync(requestUri);
            return result;
        }


        /// <summary>
        /// Json object
        /// </summary>
        /// <param name="jsonObj"></param>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> Put(string jsonObj, string requestUri)
        {
            var content = "{\"data\":" + jsonObj + "}";
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage result = await httpClient.PutAsync(requestUri, byteContent);
            //result.EnsureSuccessStatusCode();
            return result;
        }

        /// <summary>
        /// Object
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> Put(object obj, string requestUri)
        {
            JObject jObject = JObject.FromObject(obj);
            string jsonObject = jObject.ToString(Formatting.None);//JsonConvert.SerializeObject(obj)
            var content = "{\"data\":" + jsonObject + "}";
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage result = await httpClient.PutAsync(requestUri, byteContent);
            //result.EnsureSuccessStatusCode();
            return result;
        }

        public async Task<Boolean> Delete()
        {
            return true;
        }

        public async Task<Boolean> Update()
        {
            return true;
        }
    }
}
