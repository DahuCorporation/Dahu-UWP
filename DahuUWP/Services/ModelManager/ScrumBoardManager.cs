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
        // Scrum board keys on creation
        protected string OnCreationScrumBoardUuidKey = "task_board_uuid";
        protected string OnCreationScrumBoardNameKey = "task_board_name";

        // Label keys on creation
        protected string OnCreationColumnUuidKey = "task_column_uuid";
        protected string OnCreationColumnNameKey = "task_column_name";
        protected string OnCreationColumnOrderKey = "task_column_order";

        // Label keys on creation
        protected string OnCreationLabelUuidKey = "task_label_uuid";
        protected string OnCreationLabelNameKey = "name";
        protected string OnCreationLabelColorKey = "task_label_color";

        public Task<List<object>> Charge(Dictionary<string, object> routeParams)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ScrumBoard>> ChargeAllScrumBoardOfProject(string projectId)
        {
            try
            {
                ///projects/:project/taskboards
                List<ScrumBoard> scrumBoardList = new List<ScrumBoard>();
                APIService apiService = new APIService();
                string requestUri = "projects/" + projectId + "/taskboards";
                HttpResponseMessage result = await apiService.Get(requestUri, true);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        if (resp.HasValues)
                        {
                            JToken jProj = resp.First;
                            for (int i = 0; jProj != null; i++)
                            {
                                scrumBoardList.Add(jProj.ToObject<ScrumBoard>());
                                jProj = jProj.Next;
                            }
                            return scrumBoardList;
                        }
                        return null;
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

        public async Task<List<ScrumBoardColumn>> ChargeAllColumnsOfScrumBoard(string scrumBoardId)
        {
            try
            {
                List<ScrumBoardColumn> scrumBoardList = new List<ScrumBoardColumn>();
                APIService apiService = new APIService();
                string requestUri = "taskboards/" + scrumBoardId + "?extended=true";
                HttpResponseMessage result = await apiService.Get(requestUri, true);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        if (resp.HasValues)
                        {
                            JToken jProj = resp["columns"].First;
                            for (int i = 0; jProj != null; i++)
                            {
                                scrumBoardList.Add(jProj.ToObject<ScrumBoardColumn>());
                                jProj = jProj.Next;
                            }
                        }
                        return scrumBoardList;
                }
                return scrumBoardList;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Fail(ex.ToString());
                AppGeneral.UserInterfaceStatusDico["An error occured."].Display();
                return new List<ScrumBoardColumn>();
            }
        }


        public async Task<List<ScrumBoardTask>> ChargeAllTaskOfScrumBoard(string scrumBoardId)
        {
            try
            {
                List<ScrumBoardTask> scrumBoardList = new List<ScrumBoardTask>();
                APIService apiService = new APIService();
                string requestUri = "taskboards/" + scrumBoardId + "/tasks?assigned=false";
                HttpResponseMessage result = await apiService.Get(requestUri, true);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        if (resp.HasValues)
                        {
                            JToken jProj = resp.First;
                            for (int i = 0; jProj != null; i++)
                            {
                                scrumBoardList.Add(jProj.ToObject<ScrumBoardTask>());
                                jProj = jProj.Next;
                            }
                        }
                        return scrumBoardList;
                }
                return scrumBoardList;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Fail(ex.ToString());
                AppGeneral.UserInterfaceStatusDico["An error occured."].Display();
                return new List<ScrumBoardTask>();
            }
        }

        public async Task<List<ScrumBoardLabel>> ChargeAllLabelOfScrumBoard( string scrumBoardId)
        {
            try
            {
                List<ScrumBoardLabel> labelList = new List<ScrumBoardLabel>();
                APIService apiService = new APIService();
                string requestUri = "task/labels/" + scrumBoardId;
                HttpResponseMessage result = await apiService.Get(requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        JToken jProj = resp["data"].First;
                        for (int i = 0; jProj != null; i++)
                        {
                            labelList.Add(jProj.ToObject<ScrumBoardLabel>());
                            jProj = jProj.Next;
                        }
                        return labelList;
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
        /// Get all the tasks of a scrum board
        /// </summary>
        /// <param name="routeParams"></param>
        /// <param name="scrumBoardId"></param>
        /// <returns></returns>
        public async Task<List<ScrumBoardTask>> ChargeAllTaskOfColumn(string columnId)
        {
            try
            {
                List<ScrumBoardTask> taskList = new List<ScrumBoardTask>();
                APIService apiService = new APIService();
                string requestUri = "tasks/column/" + columnId;
                HttpResponseMessage result = await apiService.Get(requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        if (resp["data"] != null && resp["data"].HasValues)
                        {
                            JToken jProj = resp["data"].First;
                            for (int i = 0; jProj != null; i++)
                            {
                                taskList.Add(jProj.ToObject<ScrumBoardTask>());
                                jProj = jProj.Next;
                            }
                        }
                        return taskList;
                }
                return taskList;
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
        /// Charge on column
        /// </summary>
        /// <param name="scrumBoardId"></param>
        /// <returns></returns>
        public async Task<ScrumBoardLabel> ChargeOneLabel(string labelId)
        {
            try
            {
                APIService apiService = new APIService();
                string requestUri = "task/label/" + labelId;
                HttpResponseMessage result = await apiService.Get(requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        return ((JObject)resp["data"]).ToObject<ScrumBoardLabel>();
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
        public async Task<ScrumBoardTask> ChargeOneTask(string taskIdc)
        {
            try
            {
                APIService apiService = new APIService();
                string requestUri = "task/" + taskIdc;
                HttpResponseMessage result = await apiService.Get(requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        return ((JObject)resp["data"]).ToObject<ScrumBoardTask>();
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
        public async Task<ScrumBoard> CreateScrumBoard(object scrumBoard, string projectId)
        {
            try
            {
                APIService apiService = new APIService();
                if (String.IsNullOrWhiteSpace(projectId))
                    return null;

                string requestUri = "projects/" + projectId + "/taskboards";
                JObject jObject = new JObject
                {
                    { "name", ((ScrumBoard)scrumBoard).Name }
                };
                HttpResponseMessage result = await apiService.Post(jObject, requestUri, true);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        AppGeneral.UserInterfaceStatusDico["ScrumBoard created successfully."].Display(((ScrumBoard)scrumBoard).Name);
                        return resp.ToObject<ScrumBoard>();
                        //ScrumBoard scrumBoardReturn = new ScrumBoard
                        //{
                        //    Name = ((JObject)resp)[OnCreationScrumBoardNameKey].ToString(),
                        //    Uuid = ((JObject)resp)[OnCreationScrumBoardUuidKey].ToString()
                        //};

                        //return ((JObject)resp["data"]).ToObject<ScrumBoard>();
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
                string requestUri = "taskboards/" + scrumBoardId + "/columns";
                JObject jObject = new JObject
                {
                    { "name", ((ScrumBoardColumn)column).Name }
                };
                string jsonObject = jObject.ToString(Formatting.None);
                HttpResponseMessage result = await apiService.Post(jsonObject, requestUri, true);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        AppGeneral.UserInterfaceStatusDico["Column created successfully."].Display(((ScrumBoardColumn)column).Name);
                        return resp.ToObject<ScrumBoardColumn>();
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
        /// Create a task label, it's the color of the label
        /// </summary>
        /// <param name="column"></param>
        /// <param name="scrumBoardId"></param>
        /// <returns></returns>
        public async Task<ScrumBoardLabel> CreateTaskLabel(object taskLabel, string scrumBoardId)
        {
            try
            {
                
                APIService apiService = new APIService();
                if (String.IsNullOrWhiteSpace(scrumBoardId))
                    return null;
                string requestUri = "task/label/" + scrumBoardId;
                JObject jObject = new JObject(taskLabel);
                jObject.Add("account_uuid", AppStaticInfo.Account.Uuid);
                string jsonObject = jObject.ToString(Formatting.None);
                HttpResponseMessage result = await apiService.Post(jsonObject, requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 201:
                        AppGeneral.UserInterfaceStatusDico["Task label created successfully."].Display(((JObject)resp["data"])[OnCreationLabelNameKey].ToString());
                        ScrumBoardLabel taskLabelReturn = new ScrumBoardLabel
                        {
                            Name = ((JObject)resp["data"])[OnCreationLabelNameKey].ToString(),
                            Uuid = ((JObject)resp["data"])[OnCreationLabelUuidKey].ToString(),
                            Color = ((JObject)resp["data"])[OnCreationLabelColorKey].ToString()
                        };
                        return taskLabelReturn;

                    //return ((JObject)resp["data"]).ToObject<ScrumBoard>();
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

        public async Task<ScrumBoardTask> CreateTask(ScrumBoardTask taskLabel)
        {
            try
            {
                APIService apiService = new APIService();
                string requestUri = "taskboards/" + taskLabel.ScrumBoardUuid + "/tasks";
                //JObject jObject = new JObject(taskLabel);
                //string jsonObject = jObject.ToString(Formatting.None);
                JObject jObject = new JObject
                {
                    { "name", taskLabel.Name },
                    { "board_id", taskLabel.ScrumBoardUuid },
                    { "column_id", taskLabel.ScrumBoardColumnUuid }
                };
                HttpResponseMessage result = await apiService.Post(jObject, requestUri, true);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        AppGeneral.UserInterfaceStatusDico["Task created successfully."].Display(((JObject)resp)[OnCreationLabelNameKey].ToString());
                        return ((JObject)resp).ToObject<ScrumBoardTask>();

                    //return ((JObject)resp["data"]).ToObject<ScrumBoard>();
                    case 400:
                        // todo : Attention la description a une taille minimum
                        //TODO : différencier les erreurs, si c'est une erreur de projet deja existant ou si le uuid est incorect...
                        AppGeneral.UserInterfaceStatusDico["An error occured."].Display();
                        return new ScrumBoardTask();
                    default:
                        return new ScrumBoardTask();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Fail(ex.ToString());
                AppGeneral.UserInterfaceStatusDico["An error occured."].Display();
                return new ScrumBoardTask();
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

        public async Task<bool> DeleteScrumBoard(string scrumBoardId)
        {
            try
            {
                APIService apiService = new APIService();
                if (String.IsNullOrWhiteSpace(scrumBoardId))
                    return false;
                string requestUri = "task/board/" + scrumBoardId;

                JObject jObject = new JObject();
                jObject.Add("account_uuid", AppStaticInfo.Account.Uuid);
                string jsonObject = jObject.ToString(Formatting.None);
                HttpResponseMessage result = await apiService.Delete(jsonObject, requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        AppGeneral.UserInterfaceStatusDico["Scrum board deleted."].Display();
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

        public async Task<bool> DeleteColumn(string columnId)
        {
            try
            {
                APIService apiService = new APIService();
                if (String.IsNullOrWhiteSpace(columnId))
                    return false;
                string requestUri = "task/column/" + columnId;

                JObject jObject = new JObject();
                jObject.Add("account_uuid", AppStaticInfo.Account.Uuid);
                string jsonObject = jObject.ToString(Formatting.None);
                HttpResponseMessage result = await apiService.Delete(jsonObject, requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        AppGeneral.UserInterfaceStatusDico["Scrum board column deleted."].Display();
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

        public async Task<bool> DeleteTaskLabel(string taskLabelId)
        {
            try
            {
                APIService apiService = new APIService();
                if (String.IsNullOrWhiteSpace(taskLabelId))
                    return false;
                string requestUri = "task/label/" + taskLabelId;

                JObject jObject = new JObject();
                jObject.Add("account_uuid", AppStaticInfo.Account.Uuid);
                string jsonObject = jObject.ToString(Formatting.None);
                HttpResponseMessage result = await apiService.Delete(jsonObject, requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        AppGeneral.UserInterfaceStatusDico["Scrum board label deleted."].Display();
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

        public async Task<bool> DeleteTask(string scrumBoardId, string scrumBoardTaskId)
        {
            try
            {
                APIService apiService = new APIService();
                string requestUri = "taskboards/" + scrumBoardId + "/tasks/" + scrumBoardTaskId;
                
                HttpResponseMessage result = await apiService.DeleteBis(requestUri, true);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                switch ((int)result.StatusCode)
                {
                    case 200:
                        AppGeneral.UserInterfaceStatusDico["Scrum board task deleted."].Display();
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

        public Task<bool> Delete(string objId)
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
        /// <param name="scrumBoardId"></param>
        /// <returns></returns>
        public async Task<ScrumBoard> EditScrumBoard(object obj, string scrumBoardId)
        {
            try
            {
                APIService apiService = new APIService();
                if (String.IsNullOrWhiteSpace(scrumBoardId))
                    return null;
                string requestUri = "task/board/" + scrumBoardId;

                JObject jObject = Serialize(obj);
                jObject.Add("account_uuid", AppStaticInfo.Account.Uuid);
                string jsonObject = jObject.ToString(Formatting.None);
                HttpResponseMessage result = await apiService.Put(jsonObject, requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        AppGeneral.UserInterfaceStatusDico["Informations modified."].Display();
                        ScrumBoard scrumBoardReturn = new ScrumBoard
                        {
                            Uuid = ((JObject)resp["data"])[OnCreationScrumBoardUuidKey].ToString(),
                            Name = ((JObject)resp["data"])[OnCreationScrumBoardNameKey].ToString()
                        };
                        return scrumBoardReturn;
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
        public async Task<ScrumBoardColumn> EditColumn(object obj, string columnId)
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
                        return new ScrumBoardColumn
                        {
                            Uuid = ((JObject)resp["data"])[OnCreationColumnUuidKey].ToString(),
                            Name = ((JObject)resp["data"])[OnCreationColumnNameKey].ToString(),
                            Order = (int)((JObject)resp["data"])[OnCreationColumnOrderKey]
                        };
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
        public async Task<ScrumBoardLabel> EditTaskLabel(object obj, string taskLabelId)
        {
            try
            {
                APIService apiService = new APIService();
                if (String.IsNullOrWhiteSpace(taskLabelId))
                    return null;
                string requestUri = "task/label/" + taskLabelId;

                JObject jObject = Serialize(obj);
                jObject.Add("account_uuid", AppStaticInfo.Account.Uuid);
                string jsonObject = jObject.ToString(Formatting.None);
                HttpResponseMessage result = await apiService.Put(jsonObject, requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        AppGeneral.UserInterfaceStatusDico["Informations modified."].Display();
                        return new ScrumBoardLabel
                        {
                            Uuid = ((JObject)resp["data"])[OnCreationLabelUuidKey].ToString(),
                            Name = ((JObject)resp["data"])[OnCreationLabelNameKey].ToString(),
                            Color = ((JObject)resp["data"])[OnCreationLabelColorKey].ToString()
                        };
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

        public async Task<ScrumBoardTask> EditTask(object obj, string taskId)
        {
            try
            {
                APIService apiService = new APIService();
                if (String.IsNullOrWhiteSpace(taskId))
                    return null;
                string requestUri = "task/" + taskId;

                JObject jObject = Serialize(obj);
                jObject.Add("account_uuid", AppStaticInfo.Account.Uuid);
                string jsonObject = jObject.ToString(Formatting.None);
                HttpResponseMessage result = await apiService.Put(jsonObject, requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        AppGeneral.UserInterfaceStatusDico["Informations modified."].Display();
                        return ((JObject)resp["data"]).ToObject<ScrumBoardTask>();
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
