using DahuUWP.DahuTech;
using DahuUWP.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.Services.ModelManager
{
    public class ForumManager
    {
        /// <summary>
        /// Create a new topic
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<Topic> CreateTopic(object topic, string projectId)
        {
            try
            {
                APIService apiService = new APIService();
                if (String.IsNullOrWhiteSpace(projectId))
                    return null;
                string requestUri = "projects/" + projectId + "forum/";
                JObject jObject = new JObject
                {
                    { "name", ((Topic)topic).Name }
                };
                string jsonObject = jObject.ToString(Formatting.None);
                HttpResponseMessage result = await apiService.Post(jsonObject, requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 201:
                        AppGeneral.UserInterfaceStatusDico["Topic created successfully."].Display((JObject)resp["data"]["name"]);
                        return ((JObject)resp["data"]).ToObject<Topic>();
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

        /// <summary>
        /// Create a new message
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<bool> CreateMessage(object message, string projectId, string topicId)
        {
            try
            {
                APIService apiService = new APIService();
                if (String.IsNullOrWhiteSpace(projectId))
                    return false;
                string requestUri = "projects/" + projectId + "forum/" + topicId;
                JObject jObject = new JObject
                {
                    { "content", ((TopicMessage)message).Content }
                };
                string jsonObject = jObject.ToString(Formatting.None);
                HttpResponseMessage result = await apiService.Post(jsonObject, requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 201:
                        AppGeneral.UserInterfaceStatusDico["Topic message created successfully."].Display(((TopicMessage)message).Content);
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

        /// <summary>
        /// Charge all topics of a project
        /// </summary>
        /// <param name="scrumBoardId"></param>
        /// <returns></returns>
        public async Task<List<Topic>> ChargeAllTopicsOfProject(string projectId)
        {
            try
            {
                List<Topic> topicList = new List<Topic>();
                APIService apiService = new APIService();
                string requestUri = "projects/" + projectId + "/forum/";
                HttpResponseMessage result = await apiService.Get(requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        // Regarder si c'est pas encore le problème des 1,2,3 dans la requete de retour
                        JToken jProj = resp["data"].First;
                        for (int i = 0; jProj != null; i++)
                        {
                            topicList.Add(jProj.ToObject<Topic>());
                            jProj = jProj.Next;
                        }
                        return topicList;
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

        /// <summary>
        /// Charge one task
        /// </summary>
        /// <param name="scrumBoardId"></param>
        /// <returns></returns>
        public async Task<List<TopicMessage>> ChargeAllMessageOfTopics(string projectId, string topicId)
        {
            try
            {
                List<TopicMessage> topicMessageList = new List<TopicMessage>();
                APIService apiService = new APIService();
                string requestUri = "projects/" + projectId + "/forum/" + topicId;
                HttpResponseMessage result = await apiService.Get(requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        // Regarder si c'est pas encore le problème des 1,2,3 dans la requete de retour
                        JToken jProj = resp["data"].First;
                        for (int i = 0; jProj != null; i++)
                        {
                            topicMessageList.Add(jProj.ToObject<TopicMessage>());
                            jProj = jProj.Next;
                        }
                        return topicMessageList;
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

        /// <summary>
        /// Edit a topic
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="scrumBoardId"></param>
        /// <returns></returns>
        public async Task<bool> EditTopic(object obj, string projectId, string topicId)
        {
            try
            {
                APIService apiService = new APIService();
                if (String.IsNullOrWhiteSpace(projectId) && String.IsNullOrWhiteSpace(topicId))
                    return false;
                string requestUri = "projects/" + projectId + "/forum/" + topicId;

                JObject jObject = new JObject
                {
                    { "name", ((Topic)obj).Name }
                };
                string jsonObject = jObject.ToString(Formatting.None);
                HttpResponseMessage result = await apiService.Put(jsonObject, requestUri);
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

        /// <summary>
        /// Edit a message
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="scrumBoardId"></param>
        /// <returns></returns>
        public async Task<bool> EditMessage(object obj, string projectId, string topicId, string messageId)
        {
            try
            {
                APIService apiService = new APIService();
                if (String.IsNullOrWhiteSpace(projectId) && String.IsNullOrWhiteSpace(topicId))
                    return false;
                string requestUri = "projects/" + projectId + "/forum/" + topicId + "/" + messageId;

                JObject jObject = new JObject
                {
                    { "content", ((TopicMessage)obj).Content }
                };
                string jsonObject = jObject.ToString(Formatting.None);
                HttpResponseMessage result = await apiService.Put(jsonObject, requestUri);
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

        public async Task<bool> DeleteTopic(string projectId, string topicId)
        {
            try
            {
                APIService apiService = new APIService();
                if (String.IsNullOrWhiteSpace(projectId) && String.IsNullOrWhiteSpace(topicId))
                    return false;
                string requestUri = "projects/" + projectId + "/forum/" + topicId;

                HttpResponseMessage result = await apiService.Delete("", requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        AppGeneral.UserInterfaceStatusDico["Topic message deleted."].Display();
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

        public async Task<bool> DeleteMessage(string projectId, string topicId, string messageId)
        {
            try
            {
                APIService apiService = new APIService();
                if (String.IsNullOrWhiteSpace(projectId) && String.IsNullOrWhiteSpace(topicId) && String.IsNullOrWhiteSpace(messageId))
                    return false;
                string requestUri = "projects/" + projectId + "/forum/" + topicId + "/" + messageId;

                HttpResponseMessage result = await apiService.Delete("", requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        AppGeneral.UserInterfaceStatusDico["Topic message deleted."].Display();
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
    }
}
