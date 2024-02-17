using System.Runtime.CompilerServices;

namespace LocationAPI.Common
{
    public class Utility
    {
        /// <summary>
        /// Convert to string
        /// </summary>
        /// <param name="value">value as object</param>
        /// <returns>string output</returns>
        public string? ToString(object value) {
            return Convert.ToString(value);
        }
        /// <summary>
        /// Convert value to Init
        /// </summary>
        /// <param name="value">value as string</param>
        /// <returns>integer</returns>
        public static int ToInt(string value)
        {
            int iVal = 0;
            int.TryParse(value, out iVal);
            return iVal;
        }
        /// <summary>
        /// Convert to Time (format should be HH:mm)
        /// </summary>
        /// <param name="value">value as string</param>
        /// <returns>Timespan object</returns>
        public static TimeSpan ToTime(string value)
        {
            string[] time = value.Length > 0 ? value.Split(":") : ["0", "0"];
            return new TimeSpan(Utility.ToInt(time[0]), Utility.ToInt(time[1]),0);
        }
    }
}
