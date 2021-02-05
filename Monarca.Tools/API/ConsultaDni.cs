using Monarca.Tools.API.Models;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Monarca.Tools.API
{
    public class ConsultaDni
    {
        public async static Task<DNI> GetDni(string dni)
        {
            string endpoint = $"https://dniruc.apisperu.com/api/v1/dni/{dni}?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbWFpbCI6Imd1c3Rhdm8uZ2F2YW5jaG8ubEBnbWFpbC5jb20ifQ.E2rJjAZ93Dg5JvqH-DDwGPiO9QBoGBE110WfL7Ff0xE";
            HttpWebRequest request = WebRequest.Create(endpoint) as HttpWebRequest;
            request.Method = "GET";
            request.ContentType = "application/json";

            HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse;
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string json = await reader.ReadToEndAsync();

            DNI dniObtained = JsonConvert.DeserializeObject<DNI>(json);
            return dniObtained;
        }
    }
}
