using DahuUWP.Models.ModelManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using DahuUWP.DahuTech;
using DahuUWP.Models;

namespace DahuUWP.Services.ModelManager
{
    public class SkillManager : IModelManager
    {
        public List<object> Charge(Dictionary<string, object> routeParams)
        {
            throw new NotImplementedException();
        }

        public object ChargeOneSkill(Dictionary<string, object> routeParams)
        {
            try
            {
                APIService apiService = new APIService();
                string requestUri = "skill?";
                requestUri += string.Join("&", routeParams.Select(x => x.Key + "=" + x.Value).ToArray());
                HttpResponseMessage result = apiService.Get(requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        return resp["data"].ToObject<Skill>();
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
            try
            {
                APIService apiService = new APIService();
                string requestUri = "skill";

                JObject jObject = Serialize(obj);
                jObject.Remove("uuid");
                string jsonObject = jObject.ToString(Formatting.None);
                HttpResponseMessage result = apiService.Post(jsonObject, requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        AppGeneral.UserInterfaceStatusDico["A skill is added to the list."].Display();
                        return true;
                    case 400:
                        AppGeneral.UserInterfaceStatusDico["An error occured."].Display();
                        return false;
                    default:
                        return false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Fail(ex.ToString());
                AppGeneral.UserInterfaceStatusDico["An error occured."].Display();
                return false;
            }
        }

        public bool Delete(int objId)
        {
            throw new NotImplementedException();
        }

        public bool Edit(object obj)
        {
            throw new NotImplementedException();
        }

        public object DeSerialize(JObject jObject)
        {
            throw new NotImplementedException();
        }

        public JObject Serialize(object serializeObject)
        {
            JObject jSkill = (JObject)JToken.FromObject(serializeObject);
            return jSkill;
        }
    }

    public class DesignSkillManager : IModelManager
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

        public object DeSerialize(JObject jObject)
        {
            throw new NotImplementedException();
        }

        public bool Edit(object obj)
        {
            throw new NotImplementedException();
        }

        public JObject Serialize(object serializeObject)
        {
            throw new NotImplementedException();
        }
    }
}
