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

        public async Task<List<Skill>> ChargeAllSkills()
        {
            try
            {
                List<Skill> skillList = new List<Skill>();
                APIService apiService = new APIService();
                string requestUri = "skills";
                //requestUri += string.Join("&", routeParams.Select(x => x.Key + "=" + x.Value).ToArray());
                HttpResponseMessage result = await apiService.Get(requestUri, true);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (JArray)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        JToken jProj = resp.First;
                        for (int i = 0; jProj != null; i++)
                        {
                            skillList.Add(jProj.ToObject<Skill>());
                            jProj = jProj.Next;
                        }
                        return skillList;
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

        public async Task<object> ChargeOneSkill(string skillId)
        {
            try
            {
                APIService apiService = new APIService();
                string requestUri = "skills/" + skillId;
                //requestUri += string.Join("&", routeParams.Select(x => x.Key + "=" + x.Value).ToArray());
                HttpResponseMessage result = await apiService.Get(requestUri, true);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        return resp.ToObject<Skill>();
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

        public async Task<Skill> Create(object obj)
        {
            try
            {
                APIService apiService = new APIService();
                string requestUri = "skills";

                JObject jObject = Serialize(obj);
                jObject.Remove("uuid");
                //string jsonObject = jObject.ToString(Formatting.None);
                HttpResponseMessage result = await apiService.Post(jObject, requestUri, true);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        AppGeneral.UserInterfaceStatusDico["A skill is added to the list."].Display();
                        return resp.ToObject<Skill>();
                    case 422:
                        AppGeneral.UserInterfaceStatusDico["A skill already exists."].Display();
                        return null;
                    case 400:
                        AppGeneral.UserInterfaceStatusDico["An error occured."].Display();
                        return null;
                    default:
                        return null;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Fail(ex.ToString());
                AppGeneral.UserInterfaceStatusDico["An error occured."].Display();
                return null;
            }
        }

        public async Task<bool> Delete(string objId)
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

        Task<bool> IModelManager.Create(object obj)
        {
            throw new NotImplementedException();
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

        public Task<bool> Delete(string objId)
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
