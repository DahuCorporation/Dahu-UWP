using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DahuUWP.Utils.Enum
{
    public enum Gender
    {
        Miss,
        Sir
    };

    public static class GenderUtility
    {
        public static Gender StringToGender(string stringGender)
        {
            switch (stringGender)
            {
                case "mr":
                    return Gender.Sir;
                case "mme":
                    return Gender.Miss;
                case "Mr":
                    return Gender.Sir;
                case "Mme":
                    return Gender.Miss;
                default:
                    return Gender.Sir;
            }
        }

        public static string GenderToString(Gender gender)
        {
            switch (gender)
            {
                case Gender.Sir:
                    return "mr";
                case Gender.Miss:
                    return "mme";
                default:
                    return "mr";
            }
        }
    }
}
