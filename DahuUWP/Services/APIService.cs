using Newtonsoft.Json;
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


        /// <summary>
        /// Post to API
        /// </summary>
        /// <param name="obj">Object in body</param>
        /// <param name="requestUri">Path of api</param>
        /// <returns></returns>
        public HttpResponseMessage Post(object obj, string requestUri)
        {
            var content = "{\"data\":" + JsonConvert.SerializeObject(obj) + "}";
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage result = httpClient.PostAsync(requestUri, byteContent).Result;
            //result.EnsureSuccessStatusCode();
            return result;
        }

        public HttpResponseMessage Get(string requestUri)
        {
            
            HttpResponseMessage result = httpClient.GetAsync(requestUri).Result;
            return result;
        }

        public Boolean Delete()
        {
            return true;
        }

        public Boolean Update()
        {
            return true;
        }
    }
}
