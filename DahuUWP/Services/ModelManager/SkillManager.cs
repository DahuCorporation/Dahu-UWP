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
        public async Task<List<object>> ChargeAsync(Dictionary<string, object> routeParams)
        {
            try
            {
                List<object> skillList = new List<object>();
                APIService apiService = new APIService();
                string requestUri = "user/skill?";
                if (routeParams != null)
                    requestUri += string.Join("&", routeParams.Select(x => x.Key + "=" + x.Value).ToArray());
                HttpResponseMessage result = await apiService.Get(requestUri);
                string responseBody = await result.Content.ReadAsStringAsync();
                var resp = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        if (resp["data"] != null)
                        {
                            JToken jProj = resp["data"].First;
                            for (int i = 0; jProj != null; i++)
                            {
                                Skill presentSkill = new Skill();
                                presentSkill.Name = (string)jProj["skill_name"];
                                presentSkill.Description = (string)jProj["skill_description"];
                                skillList.Add(presentSkill);
                                //skillList.Add(jProj.ToObject<Skill>());
                                jProj = jProj.Next;
                            }
                        }
                        return skillList;
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

        public async Task<List<object>> Charge(Dictionary<string, object> routeParams)
        {
            try
            {
                List<object> skillList = new List<object>();
                APIService apiService = new APIService();
                string requestUri = "user/skill?";
                if (routeParams != null)
                    requestUri += string.Join("&", routeParams.Select(x => x.Key + "=" + x.Value).ToArray());
                HttpResponseMessage result = await apiService.Get(requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        if (resp["data"] != null)
                        {
                            JToken jProj = resp["data"].First;
                            for (int i = 0; jProj != null; i++)
                            {
                                Skill presentSkill = new Skill();
                                presentSkill.Name = (string)jProj["skill_name"];
                                presentSkill.Description = (string)jProj["skill_description"];
                                skillList.Add(presentSkill);
                                //skillList.Add(jProj.ToObject<Skill>());
                                jProj = jProj.Next;
                            }
                        }
                        return skillList;
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

        public async Task<object> ChargeOneSkill(Dictionary<string, object> routeParams)
        {
            try
            {
                APIService apiService = new APIService();
                string requestUri = "skill?";
                requestUri += string.Join("&", routeParams.Select(x => x.Key + "=" + x.Value).ToArray());
                HttpResponseMessage result = await apiService.Get(requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        return resp["data"].ToObject<Skill>();
                }
                AppGeneral.UserInterfaceStatusDico["An error occured."].Display();
                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Fail(ex.ToString());
                AppGeneral.UserInterfaceStatusDico["An error occured."].Display();
                return null;
            }
        }

        public async Task<bool> Create(object obj)
        {
            try
            {
                APIService apiService = new APIService();
                string requestUri = "skill";

                JObject jObject = Serialize(obj);
                jObject.Remove("uuid");
                string jsonObject = jObject.ToString(Formatting.None);
                HttpResponseMessage result = await apiService.Post(jsonObject, requestUri);
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

        public async Task<bool> Delete(int objId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Edit(object obj)
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
        public Task<List<object>> Charge(Dictionary<string, object> routeParams)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Create(object obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int objId)
        {
            throw new NotImplementedException();
        }

        public object DeSerialize(JObject jObject)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Edit(object obj)
        {
            throw new NotImplementedException();
        }

        public JObject Serialize(object serializeObject)
        {
            throw new NotImplementedException();
        }
    }
}
