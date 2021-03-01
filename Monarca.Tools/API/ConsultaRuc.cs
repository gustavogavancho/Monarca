using Monarca.Tools.API.Models;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Monarca.Tools.API
{
    public class ConsultaRuc
    {
        public async static Task<RUC> GetRuc(string ruc)
        {
            string endpoint = $"https://api.selvafood.com/api/consultaruc/{ruc}";
            HttpWebRequest request = WebRequest.Create(endpoint) as HttpWebRequest;
            request.Method = "GET";
            request.ContentType = "application/json";

            HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse;
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string json = await reader.ReadToEndAsync();

            RUC rucObtained = JsonConvert.DeserializeObject<RUC>(json);
            return rucObtained;
        }
    }
}
