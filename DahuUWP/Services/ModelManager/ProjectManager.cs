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
using DahuUWP.Services.ModelManager;

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
                /// users /:id / projects ? relation =: relation
                string requestUri = "projects";
                //if (routeParams != null)
                //    requestUri += string.Join("&", routeParams.Select(x => x.Key + "=" + x.Value).ToArray());
                HttpResponseMessage result = await apiService.Get(requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(responseBody);

                MediaManager mediaManager = new MediaManager();
                switch ((int)result.StatusCode)
                {
                    case 200:
                        JToken jProj = resp.First;
                        for (int i = 0; jProj != null; i++)
                        {
                            Project proj = jProj.ToObject<Project>();
                            proj.Media = await mediaManager.GetSpecificMedia(proj.Uuid);
                            projectList.Add(proj);
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

                MediaManager mediaManager = new MediaManager();
                switch ((int)result.StatusCode)
                {
                    case 200:
                        Project proj = (Project)DeSerialize((JObject)resp);
                        proj.Media = await mediaManager.GetSpecificMedia(proj.Uuid);
                        return proj;
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
                    //{ "account_uuid", AppStaticInfo.Account.Uuid },
                    { "name", ((Project)project).Name },
                    { "description", ((Project)project).Description }
                }; 
                //string jsonObject = jObject.ToString(Formatting.None);
                HttpResponseMessage result = await apiService.Post(jObject, requestUri, true);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        AppGeneral.UserInterfaceStatusDico["Project created successfully."].Display(((Project)project).Name, true);
                        return true;
                    case 400:
                        // todo : Attention la description a une taille minimum
                        //TODO : différencier les erreurs, si c'est une erreur de projet deja existant ou si le uuid est incorect...
                        AppGeneral.UserInterfaceStatusDico["An error occured."].Display();
                        return false;
                    default:
                        AppGeneral.UserInterfaceStatusDico["An error occured."].Display();
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

        public Task<bool> Delete(string objId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteUser(string projectId, string userId)
        {
            try
            {
                APIService apiService = new APIService();
                ///projects/:project_uuid/members/:user_uuid
                string requestUri = "projects/" + projectId + "/members/" + userId;

                HttpResponseMessage result = await apiService.DeleteBis(requestUri, true);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                switch ((int)result.StatusCode)
                {
                    case 200:
                        AppGeneral.UserInterfaceStatusDico["Member deleted."].Display();
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

        public async Task<bool> DeleteProject(string projectId)
        {
            try
            {
                APIService apiService = new APIService();
                ///projects/:project_uuid/members/:user_uuid
                string requestUri = "projects/" + projectId;

                HttpResponseMessage result = await apiService.DeleteBis(requestUri, true);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                switch ((int)result.StatusCode)
                {
                    case 200:
                        AppGeneral.UserInterfaceStatusDico["Project deleted."].Display();
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

        public object DeSerialize(JObject jObject)
        {
            Project project = new Project();

            project = jObject.ToObject<Project>();
            return project;
        }

        public async Task<bool> JoinProject(string projectUuid)
        {
            try
            {
                APIService apiService = new APIService();
                string requestUri = "projects/" + projectUuid + "/members";

                //string jsonObject = jObject.ToString(Formatting.None);
                HttpResponseMessage result = await apiService.Post("", requestUri, true);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                //var resp = (JArray)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        AppGeneral.UserInterfaceStatusDico["You joined the project."].Display();
                        return true;
                    case 400:
                        // todo : Attention la description a une taille minimum
                        //TODO : différencier les erreurs, si c'est une erreur de projet deja existant ou si le uuid est incorect...
                        AppGeneral.UserInterfaceStatusDico["You are already in the project."].Display();
                        return false;
                    default:
                        AppGeneral.UserInterfaceStatusDico["An error occured."].Display();
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

        public async Task<bool> EditProject(object obj, string projectUuid)
        {
            try
            {
                APIService apiService = new APIService();
                string requestUri = "projects/" + projectUuid;
                JObject jObject = Serialize(obj);
                JObject descriptionObj = new JObject()
                {
                    { "description", ((Project)obj).Description }
                };
                string jsonObject = jObject.ToString(Formatting.None);
                HttpResponseMessage result = await apiService.Put(jsonObject, requestUri); // two times because if only the description changes the back send false
                result = await apiService.Put(descriptionObj, requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        AppGeneral.UserInterfaceStatusDico["Informations modified."].Display();
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

        public Task<bool> Edit(object obj)
        {
            throw new NotImplementedException();
        }

        public JObject Serialize(object serializeObject)
        {
            return (JObject)JToken.FromObject(serializeObject);
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
