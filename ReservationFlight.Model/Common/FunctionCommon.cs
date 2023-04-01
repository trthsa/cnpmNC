namespace ReservationFlight.Model.Common
{
    public class FunctionCommon
    {
        public static string FormatTimeSpan(TimeSpan s)
        {
            return string.Format("{0:D2}:{1:D2}", s.Hours, s.Minutes);
        }

        public static string FormatMoney(decimal money)
        {
            return string.Format("{0:0,0<sup>VNĐ</sup>}", money);
        }

        public static string FormatDate(DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy");
        }

        public static string FormatLongDate(DateTime dateTime)
        {
            return dateTime.ToString("dd/MMM/yyyy");
        }
    }
}
