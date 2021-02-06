using System.Net;

namespace Monarca.UI.WPF.Usuario.Helpers
{
    public class CheckOneTimeInternetConnection
    {
        public static bool CheckInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
