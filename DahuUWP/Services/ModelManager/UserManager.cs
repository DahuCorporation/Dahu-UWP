﻿using DahuUWP.DahuTech;
using DahuUWP.Services;
using DahuUWP.Utils.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.Models.ModelManager
{
    public class UserManager : IModelManager
    {
        public List<object> Charges(Dictionary<string, object> routeParams)
        {
            try
            {
                List<object> userList = new List<object>();
                APIService apiService = new APIService();
                string requestUri = "users/";

                requestUri += AppStaticInfo.Account.Uuid + "?";
                // separateur ce met au début, 
                requestUri += string.Join("&", routeParams.Select(x => x.Key + "=" + x.Value).ToArray());
                HttpResponseMessage result = apiService.Get(requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        User user = resp["data"].ToObject<User>();
                        userList.Add(user);
                        return userList;
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

        public User Charge(Dictionary<string, object> routeParams)
        {
            try
            {
                APIService apiService = new APIService();
                string requestUri = "users/";

                requestUri += AppStaticInfo.Account.Uuid + "?";
                // separateur ce met au début, 
                requestUri += string.Join("&", routeParams.Select(x => x.Key + "=" + x.Value).ToArray());
                HttpResponseMessage result = apiService.Get(requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        return (User)DeSerialize((JObject)resp["data"]);
                        //return resp["data"].ToObject<User>();
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
                string requestUri = "users";

                JObject jObject = Serialize(obj);
                jObject.Add("password", ((User)obj).Account.Password);
                jObject.Add("type", ((User)obj).Account.Type);
                string jsonObject = jObject.ToString(Formatting.None);
                HttpResponseMessage result = apiService.Post("{\"informations\" :" + jsonObject + "}", requestUri);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        AppGeneral.UserInterfaceStatusDico["User created but not activated."].Display();
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
              try
            {
                APIService apiService = new APIService();
                string requestUri = "users";
                requestUri += "?_token=" + AppStaticInfo.Account.Token;

                JObject jObject = Serialize(obj);
                jObject.Remove("mail");
                string jsonObject = jObject.ToString(Formatting.None);
                HttpResponseMessage result = apiService.Put(jsonObject, requestUri);
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

        List<object> IModelManager.Charge(Dictionary<string, object> routeParams)
        {
            throw new NotImplementedException();
        }

        public JObject Serialize(object userToSerialize)
        {
            JObject jUser = (JObject)JToken.FromObject(userToSerialize);
            jUser.Add("gender", GenderUtility.GenderToString(((User)userToSerialize).Gender));
            return jUser;
        }

        public object DeSerialize(JObject jUser)
        {
            User user = new User();

            user = jUser.ToObject<User>();
            user.Gender = GenderUtility.StringToGender((string)jUser["gender"]);
            return user;
        }
    }

    public class DesignUserManager : IModelManager
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
