namespace Panda.Extensions
{
    using System;
    using System.Globalization;

    public static class DateTimeExtension
    {
        public static string ToDate(this DateTime date)
        {
            return date.ToString("dd/MM/yyyy", CultureInfo.GetCultureInfo("en-EN"));
        }
    }
}
