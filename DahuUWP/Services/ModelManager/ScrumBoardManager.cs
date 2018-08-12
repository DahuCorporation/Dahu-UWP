using DahuUWP.DahuTech;
using DahuUWP.Models;
using DahuUWP.Models.ModelManager;
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
    public class ScrumBoardManager : IModelManager
    {
        public Task<List<object>> Charge(Dictionary<string, object> routeParams)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ScrumBoard>> ChargeAllScrumBoardOfProject(Dictionary<string, object> routeParams, string projectId)
        {
            try
            {
                List<ScrumBoard> scrumBoardList = new List<ScrumBoard>();
                APIService apiService = new APIService();
                string requestUri = "task/boards/" + projectId;
                HttpResponseMessage result = await apiService.Get(requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        JToken jProj = resp["data"].First;
                        for (int i = 0; jProj != null; i++)
                        {
                            scrumBoardList.Add(jProj.ToObject<ScrumBoard>());
                            jProj = jProj.Next;
                        }
                        return scrumBoardList;
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

        public async Task<List<ScrumBoardColumn>> ChargeAllColumnsOfScrumBoard(Dictionary<string, object> routeParams, string scrumBoardId)
        {
            try
            {
                List<ScrumBoardColumn> scrumBoardList = new List<ScrumBoardColumn>();
                APIService apiService = new APIService();
                string requestUri = "task/columns/" + scrumBoardId;
                HttpResponseMessage result = await apiService.Get(requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        JToken jProj = resp["data"].First;
                        for (int i = 0; jProj != null; i++)
                        {
                            scrumBoardList.Add(jProj.ToObject<ScrumBoardColumn>());
                            jProj = jProj.Next;
                        }
                        return scrumBoardList;
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
        /// Charge one scrum board
        /// </summary>
        /// <param name="scrumBoardId"></param>
        /// <returns></returns>
        public async Task<ScrumBoard> ChargeOneScrumBoard(string scrumBoardId)
        {
            try
            {
                APIService apiService = new APIService();
                string requestUri = "task/board/" + scrumBoardId;
                HttpResponseMessage result = await apiService.Get(requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        return (ScrumBoard)DeSerialize((JObject)resp["data"]);
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
        /// Charge on column
        /// </summary>
        /// <param name="scrumBoardId"></param>
        /// <returns></returns>
        public async Task<ScrumBoardColumn> ChargeOneColumn(string columnId)
        {
            try
            {
                APIService apiService = new APIService();
                string requestUri = "task/column/" + columnId;
                HttpResponseMessage result = await apiService.Get(requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        return ((JObject)resp["data"]).ToObject<ScrumBoardColumn>();
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
        /// Create a new scrum board
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<ScrumBoard> CreateAScrumBoard(object scrumBoard, string projectId)
        {
            try
            {
                APIService apiService = new APIService();
                if (String.IsNullOrWhiteSpace(projectId))
                    return null;
                string requestUri = "task/board/" + projectId;
                JObject jObject = new JObject
                {
                    { "account_uuid", AppStaticInfo.Account.Uuid },
                    { "name", ((ScrumBoard)scrumBoard).Name }
                };
                string jsonObject = jObject.ToString(Formatting.None);
                HttpResponseMessage result = await apiService.Post(jsonObject, requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 201:
                        AppGeneral.UserInterfaceStatusDico["ScrumBoard created successfully."].Display(((ScrumBoard)scrumBoard).Name);

                        return (ScrumBoard)DeSerialize((JObject)resp["data"]);
                    case 400:
                        // todo : Attention la description a une taille minimum
                        //TODO : différencier les erreurs, si c'est une erreur de projet deja existant ou si le uuid est incorect...
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
        /// Create a new column to scrum board
        /// </summary>
        /// <param name="scrumBoard"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<ScrumBoardColumn> CreateAColumn(object column, string scrumBoardId)
        {
            try
            {
                APIService apiService = new APIService();
                if (String.IsNullOrWhiteSpace(scrumBoardId))
                    return null;
                string requestUri = "task/column/" + scrumBoardId;
                JObject jObject = new JObject
                {
                    { "account_uuid", AppStaticInfo.Account.Uuid },
                    { "name", ((ScrumBoardColumn)column).Name }
                };
                string jsonObject = jObject.ToString(Formatting.None);
                HttpResponseMessage result = await apiService.Post(jsonObject, requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 201:
                        AppGeneral.UserInterfaceStatusDico["Column created successfully."].Display(((ScrumBoardColumn)column).Name);
                        return ((JObject)resp["data"]).ToObject<ScrumBoardColumn>();
                    case 400:
                        // todo : Attention la description a une taille minimum
                        //TODO : différencier les erreurs, si c'est une erreur de projet deja existant ou si le uuid est incorect...
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
        /// This method should not be used
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
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
            ScrumBoard scrumBoard = new ScrumBoard();

            scrumBoard = jObject.ToObject<ScrumBoard>();
            return scrumBoard;
        }

        /// <summary>
        /// Edit a scrum board
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<ScrumBoardColumn> EditScrumBoard(object obj, string projectId)
        {
            try
            {
                APIService apiService = new APIService();
                if (String.IsNullOrWhiteSpace(projectId))
                    return null;
                string requestUri = "task/column/" + projectId;

                JObject jObject = Serialize(obj);
                string jsonObject = jObject.ToString(Formatting.None);
                HttpResponseMessage result = await apiService.Put(jsonObject, requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        AppGeneral.UserInterfaceStatusDico["Informations modified."].Display();
                        return ((JObject)resp["data"]).ToObject<ScrumBoardColumn>();
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
        /// Edit a column
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<ScrumBoard> EditColumn(object obj, string columnId)
        {
            try
            {
                APIService apiService = new APIService();
                if (String.IsNullOrWhiteSpace(columnId))
                    return null;
                string requestUri = "task/board/" + columnId;

                JObject jObject = Serialize(obj);
                string jsonObject = jObject.ToString(Formatting.None);
                HttpResponseMessage result = await apiService.Put(jsonObject, requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        AppGeneral.UserInterfaceStatusDico["Informations modified."].Display();
                        return (ScrumBoardColumn)DeSerialize((JObject)resp["data"]);
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
        /// This method should not be used
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Task<bool> Edit(object obj)
        {
            throw new NotImplementedException();
        }

        public JObject Serialize(object serializeObject)
        {
            JObject jScrumBoard = (JObject)JToken.FromObject(serializeObject);
            return jScrumBoard;
        }
    }
}
