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
    public class CounterpartsManager : IModelManager
    {
        public Task<List<object>> Charge(Dictionary<string, object> routeParams)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Counterpart>> ChargeCounterpartsOfProject(string projectId)
        {
            List<Counterpart> counterpartList = new List<Counterpart>();
            try
            {
                APIService apiService = new APIService();
                /// users /:id / projects ? relation =: relation
                string requestUri = "projects/" + projectId + "/counterparts";
                //if (routeParams != null)
                //    requestUri += string.Join("&", routeParams.Select(x => x.Key + "=" + x.Value).ToArray());
                HttpResponseMessage result = await apiService.Get(requestUri, true);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(responseBody);

                MediaManager mediaManager = new MediaManager();
                switch ((int)result.StatusCode)
                {
                    case 200:
                        JToken jCounterpart = resp.First;
                        for (int i = 0; jCounterpart != null; i++)
                        {
                            Counterpart counterpart = jCounterpart.ToObject<Counterpart>();
                            if (counterpart.Amount.Length > 2)
                                counterpart.Amount = (Int32.Parse(counterpart.Amount) / 100).ToString();
                            counterpartList.Add(counterpart);
                            jCounterpart = jCounterpart.Next;
                        }
                        return counterpartList;
                }
                return counterpartList;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Fail(ex.ToString());
                AppGeneral.UserInterfaceStatusDico["An error occured."].Display();
                return counterpartList;
            }
        }

        public async Task<Counterpart> Create(string amount, string description, string projectId)
        {
            Counterpart counterpart = new Counterpart();
            try
            {
                APIService apiService = new APIService();
                string requestUri = "projects/" + projectId + "/counterparts";

                JObject jObject = new JObject
                {
                    { "description", description },
                    { "amount", amount }
                };
                //"{\"description\": \"bla bla bla 20\",\"amount\": \"20\"}"
                //string jsonObject = jObject.ToString(Formatting.None);
                HttpResponseMessage result = await apiService.Post(jObject, requestUri, true);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 201:
                        counterpart = ((JObject)resp).ToObject<Counterpart>();
                        AppGeneral.UserInterfaceStatusDico["Counterpart created successfully."].Display();
                        return counterpart;
                    case 400:
                        // todo : Attention la description a une taille minimum
                        //TODO : différencier les erreurs, si c'est une erreur de projet deja existant ou si le uuid est incorect...
                        AppGeneral.UserInterfaceStatusDico["An error occured."].Display();
                        return counterpart;
                    default:
                        AppGeneral.UserInterfaceStatusDico["An error occured."].Display();
                        return counterpart;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Fail(ex.ToString());
                AppGeneral.UserInterfaceStatusDico["An error occured."].Display();
                return counterpart;
            }
        }

        public async Task<bool> CreateCharge(CardCharge cardCharge, string projectId)
        {
            Counterpart counterpart = new Counterpart();
            try
            {
                cardCharge.Amount = cardCharge.Amount + "00";
                APIService apiService = new APIService();
                string requestUri = "projects/" + projectId + "/charges";

                JObject jObject = (JObject)JToken.FromObject(cardCharge);
                //"{\"description\": \"bla bla bla 20\",\"amount\": \"20\"}"
                //string jsonObject = jObject.ToString(Formatting.None);
                HttpResponseMessage result = await apiService.Post(jObject, requestUri, true);
                string responseBody = result.Content.ReadAsStringAsync().Result;
                var resp = (JObject)JsonConvert.DeserializeObject(responseBody);
                switch ((int)result.StatusCode)
                {
                    case 200:
                        AppGeneral.UserInterfaceStatusDico["Your payment has been accepted."].Display();
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
