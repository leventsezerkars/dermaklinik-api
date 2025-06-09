namespace DermaKlinik.API.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime FirstDayOfMonth(this DateTime value) => new DateTime(value.Year, value.Month, 1);

        public static int DaysInMonth(this DateTime value) => DateTime.DaysInMonth(value.Year, value.Month);

        public static DateTime LastDayOfMonth(this DateTime value) => new DateTime(value.Year, value.Month, value.DaysInMonth());

        public static DateTime AddSmartMonths(this DateTime value, int numberOfMonths)
        {
            if (DateTime.DaysInMonth(value.Year, value.Month) != value.Day)
                return value.AddMonths(numberOfMonths);
            DateTime dateTime = value.AddMonths(numberOfMonths);
            return new DateTime(dateTime.Year, dateTime.Month, DateTime.DaysInMonth(dateTime.Year, dateTime.Month));
        }
    }
}
