using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using DahuUWP.Services;
using System.Net.Http;
using Newtonsoft.Json;
using DahuUWP.DahuTech;

namespace DahuUWP.Models.ModelManager
{
    public class ProjectManager : IModelManager
    {
        public async Task<List<object>> Charge(Dictionary<string, object> routeParams)
        {
            try
            {
                List<object> projectList = new List<object>();
                APIService apiService = new APIService();
                string requestUri = "projects/";
                if (routeParams != null)
                    requestUri += string.Join("&", routeParams.Select(x => x.Key + "=" + x.Value).ToArray());
                HttpResponseMessage result = await apiService.Get(requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        JToken jProj = resp["data"].First;
                        for (int i = 0; jProj != null; i++)
                        {
                            projectList.Add(jProj.ToObject<Project>());
                            jProj = jProj.Next;
                        }
                        return projectList;
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

        public async Task<Project> ChargeOneProject(string projectId)
        {
            try
            {
                APIService apiService = new APIService();
                string requestUri = "projects/" + projectId;
                HttpResponseMessage result = await apiService.Get(requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        return (Project)DeSerialize((JObject)resp["data"]);
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

        public async Task<bool> Create(object project)
        {
            try
            {
                APIService apiService = new APIService();
                string requestUri = "projects";

                JObject jObject = new JObject
                {
                    { "account_uuid", AppStaticInfo.Account.Uuid },
                    { "name", ((Project)project).Name },
                    { "description", ((Project)project).Description }
                }; 
                string jsonObject = jObject.ToString(Formatting.None);
                HttpResponseMessage result = await apiService.Post(jsonObject, requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        AppGeneral.UserInterfaceStatusDico["Project created successfully."].Display(((Project)project).Name);
                        return true;
                    case 400:
                        // todo : Attention la description a une taille minimum
                        //TODO : différencier les erreurs, si c'est une erreur de projet deja existant ou si le uuid est incorect...
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

        public Task<bool> Delete(int objId)
        {
            throw new NotImplementedException();
        }

        public object DeSerialize(JObject jObject)
        {
            Project project = new Project();

            project = jObject.ToObject<Project>();
            return project;
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

    public class DesignProjectManager : IModelManager
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
