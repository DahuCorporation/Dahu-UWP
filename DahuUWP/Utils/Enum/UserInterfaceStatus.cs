using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.Utils.Enum
{
    class UserInterfaceStatus
    {
        public enum Connection
        {
            InvalidEmail,
            InvalidPassword,
            InvalidEmailAndPassword,
            InvalidEmailOrPassword,
            EmptyEmail,
            EmptyPassword,
            EmptyEmailAndPasword
        };
    }
}
