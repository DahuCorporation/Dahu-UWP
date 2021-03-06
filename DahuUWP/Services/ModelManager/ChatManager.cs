﻿using DahuUWP.DahuTech;
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
    class ChatManager : IModelManager
    {
        public Task<List<object>> Charge(Dictionary<string, object> routeParams)
        {
            throw new NotImplementedException();
        }
        public Task<bool> Create(object obj)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateMessage(string message, string projectId)
        {
            try
            {
                APIService apiService = new APIService();
                if (String.IsNullOrWhiteSpace(projectId))
                    return false;

                string requestUri = "projects/" + projectId + "/messages";
                JObject jObject = new JObject
                {
                    { "message", message }
                };
                HttpResponseMessage result = await apiService.Post(jObject, requestUri, true);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 202:
                        //AppGeneral.UserInterfaceStatusDico["ScrumBoard created successfully."].Display(((ScrumBoard)scrumBoard).Name);
                        return true;
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

        public async Task<List<Chat>> ChargeMessages(string projectUuid, int offset, int limit)
        {
            List<Chat> chatList = new List<Chat>();
            try
            {
                
                APIService apiService = new APIService();
                string requestUri = "projects/" + projectUuid + "/messages?offset=" + offset + "&limit=" + limit;
                //if (routeParams != null)
                //    requestUri += string.Join("&", routeParams.Select(x => x.Key + "=" + x.Value).ToArray());
                HttpResponseMessage result = await apiService.Get(requestUri, true);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        JToken jProj = resp.First;
                        for (int i = 0; jProj != null; i++)
                        {
                            chatList.Add(jProj.ToObject<Chat>());
                            jProj = jProj.Next;
                        }
                        return chatList;
                }
                return chatList;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Fail(ex.ToString());
                AppGeneral.UserInterfaceStatusDico["An error occured."].Display();
                return chatList;
            }
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
