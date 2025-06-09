using System;
using System.Text;

namespace DermaKlinik.API.Core.Extensions
{
    public static class TimeSpanExtensions
    {
        public static string ToShortTimeString(this TimeSpan value, bool second = true) => second ? value.ToString("hh\\:mm\\:ss") : value.ToString("hh\\:mm");

        public static string Humanize(this TimeSpan? duration) => !duration.HasValue ? null : duration.Value.Humanize();

        public static string Humanize(this TimeSpan duration)
        {
            StringBuilder stringBuilder = new StringBuilder();
            duration = duration.Duration();
            if (duration.Days > 0)
                stringBuilder.Append(string.Format("{0} gün ", duration.Days));
            if (duration.Hours > 0)
                stringBuilder.Append(string.Format("{0} saat ", duration.Hours));
            if (duration.Minutes > 0)
                stringBuilder.Append(string.Format("{0} dk ", duration.Minutes));
            if (duration.TotalHours < 1.0)
            {
                if (duration.Seconds > 0)
                {
                    stringBuilder.Append(duration.Seconds);
                    if (duration.Milliseconds > 0)
                        stringBuilder.Append("." + duration.Milliseconds.ToString().PadLeft(3, '0'));
                    stringBuilder.Append(" sn ");
                }
                else if (duration.Milliseconds > 0)
                    stringBuilder.Append(string.Format("{0} ms ", duration.Milliseconds));
            }
            if (stringBuilder.Length <= 1 && duration.TotalMilliseconds != 0.0)
                stringBuilder.Append(" <1ms ");
            if (stringBuilder.Length >= 1)
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
            return stringBuilder.ToString();
        }
    }
}
