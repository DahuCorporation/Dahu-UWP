using DahuUWP.DahuTech;
using DahuUWP.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.Models.ModelManager
{
    public class UserManager : IModelManager
    {
        public List<object> Charges(Dictionary<string, object> routeParams)
        {
            try
            {
                List<object> userList = new List<object>();
                APIService apiService = new APIService();
                string requestUri = "users/";

                requestUri += AppStaticInfo.Account.Uuid + "?";
                // separateur ce met au début, 
                requestUri += string.Join("&", routeParams.Select(x => x.Key + "=" + x.Value).ToArray());
                HttpResponseMessage result = apiService.Get(requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        User user = resp["data"].ToObject<User>();
                        userList.Add(user);
                        return userList;
                }
                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Fail(ex.ToString());
                AppGeneral.UserInterfaceStatusDico["An error occured."].Display();
                return null;
            }
        }

        public User Charge(Dictionary<string, object> routeParams)
        {
            try
            {
                APIService apiService = new APIService();
                string requestUri = "users/";

                requestUri += AppStaticInfo.Account.Uuid + "?";
                // separateur ce met au début, 
                requestUri += string.Join("&", routeParams.Select(x => x.Key + "=" + x.Value).ToArray());
                HttpResponseMessage result = apiService.Get(requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        return resp["data"].ToObject<User>();
                }
                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Fail(ex.ToString());
                AppGeneral.UserInterfaceStatusDico["An error occured."].Display();
                return null;
            }

        }

        public bool Create(object obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int objId)
        {
            throw new NotImplementedException();
        }

        public bool Edit(object obj)
        {
            throw new NotImplementedException();
        }

        List<object> IModelManager.Charge(Dictionary<string, object> routeParams)
        {
            throw new NotImplementedException();
        }
    }

    public class DesignUserManager : IModelManager
    {
        public List<object> Charge(Dictionary<string, object> routeParams)
        {
            throw new NotImplementedException();
        }

        public bool Create(object obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int objId)
        {
            throw new NotImplementedException();
        }

        public bool Edit(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
