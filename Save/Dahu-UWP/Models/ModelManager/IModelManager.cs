using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dahu_UWP.Models.ModelManager
{
    interface IModelManager
    {
        Boolean Create(Object obj);
        Boolean Edit(Object obj);
        Boolean Delete(int objId);
        List<Object> Charge();
    }
}
