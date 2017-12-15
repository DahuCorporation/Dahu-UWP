using DahuUWP.Services;
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
        public List<object> Charge(Dictionary<string, object> routeParams)
        {
            APIService apiService = new APIService();
            string requestUri = "users/";

            requestUri += AppStaticInfo.Account.Uuid;
            // separateur ce met au début, 
            requestUri += string.Join("&", routeParams.Select(x => x.Key + "=" + x.Value).ToArray());
            HttpResponseMessage result = apiService.Get(requestUri);
            string responseBody = result.Content.ReadAsStringAsync().Result;
            return null;
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
