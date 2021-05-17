using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clima_Tempo.Extensions
{
    public static class DateExtension
    {
        public static string ToPtBr(this DayOfWeek dayOfWeek)
        {
            var culture = new System.Globalization.CultureInfo("pt-BR");
            var day = culture.DateTimeFormat.GetDayName(dayOfWeek);
            return day;
        }
    }
}