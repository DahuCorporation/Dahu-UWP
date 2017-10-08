using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace Dahu_UWP.Services
{
    class APIService
    {
        private String route = "http://fncs.eu/api/forward/";
        private HttpClient httpClient = new HttpClient();
        private String jsonBody;

        /// <summary>
        /// Add a route extension to the API route
        /// </summary>
        /// <param name="routeExtension">String extension you want to add</param>
        /// <returns></returns>
        public Boolean AddToRoute(string routeExtension)
        {
            if (!String.IsNullOrWhiteSpace(routeExtension))
            {
                route = String.Concat(routeExtension);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Fill the body in a json string with the obj sended
        /// </summary>
        /// <param name="obj">Object you want to be in the body</param>
        /// <returns></returns>
        public Boolean JsonBodyContent(Object obj)
        {
            if (obj != null)
            {
                jsonBody = "{\"data\":{" + JsonConvert.SerializeObject(obj) + "}}";
                return true;
            }
            return false;
        }

        public async Task<bool> PostAsync()
        {
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage wcfResponse = await httpClient.PostAsync(route, new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            return true;
        }

        public Boolean Get()
        {
            return true;
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
