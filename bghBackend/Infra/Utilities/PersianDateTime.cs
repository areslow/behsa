using System.Globalization;

namespace bghBackend.Infra.Utilities
{

    /// <summary>
    /// return the date time based in persian date calendar
    /// </summary>
    public static class PersianDateTime
    {
        public static string Now()
        {
            try
            {
                return Convert.ToDateTime(DateTime.Now).ToString("yyyy/MM/dd:HH:mm:ss", new CultureInfo("fa-IR"));
            }
            catch (Exception ex)
            {
                throw new Exception("Can not convert the provided date: \n" + ex.Message);
            }
            
        }
    }





}
