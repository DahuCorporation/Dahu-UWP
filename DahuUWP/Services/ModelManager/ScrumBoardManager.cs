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
