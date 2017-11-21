using DahuUWP.Services;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;
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

        [JsonProperty(PropertyName = "mail")]
        public string Mail2 { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password2 { get; set; }

        [JsonProperty(PropertyName = "uuid")]
        public string Uuid { get; set; }

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
        public bool Connect()
        {
            try
            {

                //HttpClient client = new HttpClient();

                //client.BaseAddress = new Uri("http://fncs.eu/api/forward/auth");
                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //object connection = new
                //{
                //    mail = (string)AppStaticInfo.Account.GetType().GetProperty("Mail").GetValue(AppStaticInfo.Account),
                //    password = (string)AppStaticInfo.Account.GetType().GetProperty("Password").GetValue(AppStaticInfo.Account)
                //};

                //var myContent = "{\"data\":" + JsonConvert.SerializeObject(connection) + "}";
                //var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                //var byteContent = new ByteArrayContent(buffer);
                //byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                //HttpResponseMessage result = client.PostAsync("", byteContent).Result;
                //result.EnsureSuccessStatusCode();
                //return true;
                APIService service = new APIService();

                object connection = new
                {
                    mail = (string)AppStaticInfo.Account.GetType().GetProperty("Mail").GetValue(AppStaticInfo.Account),
                    password = (string)AppStaticInfo.Account.GetType().GetProperty("Password").GetValue(AppStaticInfo.Account)
                };
                HttpResponseMessage result = service.Post(connection, "auth");
                string responseBody = result.Content.ReadAsStringAsync().Result;
                return true;
            }
            catch //(Exception e) //Task<bool> task = Task.Run<bool>(async () => await service.PostAsync());
            {
                return false;
            }
        }

        public void Disconnect()
        {

        }
    }
}
