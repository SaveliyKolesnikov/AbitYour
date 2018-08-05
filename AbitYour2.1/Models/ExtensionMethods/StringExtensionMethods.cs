using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbitYour.Models.ExtensionMethods
{
    public static class StringExtensionMethods
    {
        public static double ParseToDouble(this string value)
        {
            value = value.Trim();

            if (!double.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.GetCultureInfo("ru-RU"), out var result))
            {
                if (!double.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.GetCultureInfo("en-US"), out result))
                {
                    return double.NaN;
                }
            }

            return result;
        }
    }
}
