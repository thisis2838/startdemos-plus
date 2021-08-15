using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static startdemos_ui.MainForm;

namespace startdemos_ui.Utils
{
    static class EnumHelper
    {
        public static T ParseEnum<T>(string input) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), input);
        }
    }
}
