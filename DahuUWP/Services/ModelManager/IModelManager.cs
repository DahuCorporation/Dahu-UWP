using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.Models.ModelManager
{
    interface IModelManager
    {
        /// <summary>
        /// Post method
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<Boolean> Create(Object obj);

        /// <summary>
        /// Put method
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<Boolean> Edit(Object obj);

        /// <summary>
        /// Delete method
        /// </summary>
        /// <param name="objId"></param>
        /// <returns></returns>
        Task<Boolean> Delete(string objId);

        /// <summary>
        /// Get methode
        /// </summary>
        /// <returns></returns>
        Task<List<Object>> Charge(Dictionary<string, object> routeParams);

        /// <summary>
        /// Serialize a object
        /// </summary>
        /// <returns></returns>
        JObject Serialize(object serializeObject);

        /// <summary>
        /// Deserialize a object
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        object DeSerialize(JObject jObject);
    }
}
