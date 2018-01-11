using DahuUWP.DahuTech;
using DahuUWP.DahuTech.ViewNotification;
using DahuUWP.Models.ModelManager;
using DahuUWP.Services;
using DahuUWP.Utils;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.Models
{
    public class Account : ObservableObject
    {
        private string mail;

        [JsonProperty(PropertyName = "mail")]
        public string Mail
        {
            get { return mail; }
            set
            {
                if (value != mail)
                {
                    mail = value;
                    RaisePropertyChanged("Mail");
                }
            }
        }

        private string password;

        [JsonProperty(PropertyName = "password")]
        public string Password
        {
            get { return password; }
            set
            {
                if (value != password)
                {
                    password = value;
                    RaisePropertyChanged("Password");
                }
            }
        }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }


        private string _uuid;
        [JsonProperty(PropertyName = "uuid")]
        public string Uuid { get { return _uuid; } set { _uuid = value; } }

        private string _token;
        public string Token { get { return _token; } set { _token = value; } }

        [JsonProperty(PropertyName = "cgu")]
        public int Cgu { get; set; }

        [JsonProperty(PropertyName = "validate")]
        public bool Validate { get; set; }

        /// <summary>
        /// A changer en type(enum : user, enterprise)
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "picture")]
        public string Picture { get; set; }
    }

    public class AccountDataService
    {
        /// <summary>
        /// True if ok, false if error
        /// </summary>
        /// <returns></returns>
        public bool Connect()
        {
            object connection = new
            {
                mail = (string)AppStaticInfo.Account.Mail,
                password = (string)AppStaticInfo.Account.Password
            };
            return Connect(connection);
        }

        public bool Connect(object connection)
        {
            try
            {
                APIService service = new APIService();
                //AppStaticInfo.Account = new Account();

                HttpResponseMessage result = service.Post(connection, "auth");
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    //success
                    case 200:
                        UserManager userManager = (UserManager)ViewModelLocator.CurrentViewModel.dataService.GetUserManager();
                        AppStaticInfo.Account.Uuid = (string)resp["data"]["uuid"];
                        AppStaticInfo.Account.Token = (string)resp["data"]["_token"];

                        Dictionary<string, object> userDicoCharge = new Dictionary<string, object>
                        {
                            { "_token", AppStaticInfo.Account.Token }
                        };
                        User user = userManager.Charge(userDicoCharge);
                        if (user == null)
                            return false;
                        AppGeneral.UserInterfaceStatusDico["Connection success."].Display(user.FirstName);
                        return true;
                    case 400:
                        AppGeneral.UserInterfaceStatusDico[(string)resp["message"]].Display();
                        //List<string> LocalApiToUserMsg = new List<string>
                        //{
                        //    (string)resp["message"]
                        //};
                        //AppGeneral.ApiToUserMsg = LocalApiToUserMsg;
                        return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Fail(ex.ToString());
                AppGeneral.UserInterfaceStatusDico["An error occured."].Display();
                return false;
            }
        }

        public void Disconnect()
        {

        }
    }
}
