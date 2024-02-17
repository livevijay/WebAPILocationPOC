using System.Runtime.CompilerServices;

namespace LocationAPI.Common
{
    public class Utility
    {
        public string? ToString(object value) {
            return Convert.ToString(value);
        }
        public static int ToInt(string value)
        {
            int iVal = 0;
            int.TryParse(value, out iVal);
            return iVal;
        }
        public static TimeSpan ToTime(string value)
        {
            string[] time = value.Length > 0 ? value.Split(":") : ["0", "0"];
            return new TimeSpan(Utility.ToInt(time[0]), Utility.ToInt(time[1]),0);
        }
    }
}
