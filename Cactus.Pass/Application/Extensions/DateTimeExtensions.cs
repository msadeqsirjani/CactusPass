using System;
using System.Globalization;

namespace Application.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToPersianDateTime(this DateTime @this)
        {
            var persianCalendar = new PersianCalendar();

            return persianCalendar.GetYear(@this) + "/"
                                                  + persianCalendar.GetMonth(@this).ToString("00") + "/"
                                                  + persianCalendar.GetDayOfMonth(@this).ToString("00") + " "
                                                  + persianCalendar.GetHour(@this).ToString("00") + ":"
                                                  + persianCalendar.GetMinute(@this).ToString("00") + ":"
                                                  + persianCalendar.GetSecond(@this).ToString("00");
        }

    }
}
