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
    class MediaManager : IModelManager
    {
        public Task<List<object>> Charge(Dictionary<string, object> routeParams)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Create(byte[] picture, string nameForBinding)
        {
            try
            {
                APIService apiService = new APIService();
                string requestUri = "medias/";

                JObject jObject = new JObject
                {
                    { "image", picture },
                    { "name_for_user", nameForBinding }
                };


                //new ByteArrayContent(picture, 0, picture.Count()), file.DisplayName, file.Name);

                //MultipartFormDataContent form = new MultipartFormDataContent();

                //form.Add(new StringContent(json), "payload");
                //form.Add(new ByteArrayContent(picture, 0, picture.Count()), "user_picture", "user_picture.jpg");
                //string jsonObject = jObject.ToString(Formatting.None);
                HttpResponseMessage result = await apiService.Post(jObject, requestUri, true);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        //AppGeneral.UserInterfaceStatusDico["Project created successfully."].Display(((Project)project).Name, true);
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

        public async Task<Media> GetSpecificMedia(string nameForBinding)
        {
            Media basicMedia = new Media()
            {
                NameBinding = "",
                ImagePath = "https://cdn.pixabay.com/photo/2013/04/06/11/50/image-editing-101040_960_720.jpg",
                Type = "jpg",
                UserUuid = "",
                Uuid = ""
            };
            try
            {
                List<Media> mediatList = new List<Media>();
                APIService apiService = new APIService();
                string requestUri = "medias";
                
                HttpResponseMessage result = await apiService.Get(requestUri, true);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        JToken jProj = resp.First;
                        for (int i = 0; jProj != null; i++)
                        {
                            mediatList.Add(jProj.ToObject<Media>());
                            jProj = jProj.Next;
                        }
                        foreach (Media elem in mediatList)
                        {
                            if (elem.NameBinding == nameForBinding)
                                return elem;
                        }
                        return basicMedia;
                        
                }
                return basicMedia;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Fail(ex.ToString());
                AppGeneral.UserInterfaceStatusDico["An error occured."].Display();
                return basicMedia;
            }
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
