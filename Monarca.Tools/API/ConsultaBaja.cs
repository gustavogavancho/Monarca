using Monarca.COMMON.Entidades;
using Monarca.Tools.API.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Monarca.Tools.API
{
    public class ConsultaBaja
    {
        public static async Task<BajaResponse> Baja_factura(Venta venta)
        {
            string url = $"https://facturacion.selvafood.com/api/voided";
            BajaFacturaRequest body = new BajaFacturaRequest
            {
                fecha_de_emision_de_documentos = venta.FechaHoraCreacion.ToString("yyyy-MM-dd"),
                documentos = new System.Collections.Generic.List<DocumentoFactura>
                {
                    new DocumentoFactura
                    {
                        external_id = venta.ExternalId,
                        motivo_anulacion = "Se duplico documento",
                    },
                }
            };

            string json = JsonConvert.SerializeObject(body);

            dynamic jsoncheck = JsonConvert.DeserializeObject(json);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            string responseContent = "";

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("authorization", "Bearer xtu5401IsF1KEH5IsDUNk5CrNhqdsUEArEcAUBGG5Q8y27E9Mn");
                // Do the actual request and await the response
                var httpResponse = await httpClient.PostAsync(url, httpContent);

                // If the response contains content we want to read it!
                if (httpResponse.Content != null)
                {
                    responseContent = await httpResponse.Content.ReadAsStringAsync();

                    var response = JsonConvert.DeserializeObject<BajaResponse>(responseContent);
                    //  Debug.WriteLine(responseContent);

                    //     Debug.WriteLine(responseContent);
                    // From here on you could deserialize the ResponseContent back again to a concrete C# type using Json.Net
                    return response;
                }
                return null;
            }
        }

        public static async Task<BajaResponse> Baja_boleta(Venta venta)
        {
            string url = $"https://facturacion.selvafood.com/api/summaries";
            BajaBoletaRequest body = new BajaBoletaRequest
            {
                fecha_de_emision_de_documentos = venta.FechaHoraCreacion.ToString("yyyy-MM-dd"),
                codigo_tipo_proceso = "3",
                documentos = new System.Collections.Generic.List<DocumentoBoleta>
                {
                    new DocumentoBoleta
                    {
                        external_id = venta.ExternalId,
                        motivo_anulacion = "Se duplico documento",
                    },
                }
            };

            string json = JsonConvert.SerializeObject(body);

            dynamic jsoncheck = JsonConvert.DeserializeObject(json);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            string responseContent = "";

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("authorization", "Bearer xtu5401IsF1KEH5IsDUNk5CrNhqdsUEArEcAUBGG5Q8y27E9Mn");
                // Do the actual request and await the response
                var httpResponse = await httpClient.PostAsync(url, httpContent);

                // If the response contains content we want to read it!
                if (httpResponse.Content != null)
                {
                    responseContent = await httpResponse.Content.ReadAsStringAsync();

                    var response = JsonConvert.DeserializeObject<BajaResponse>(responseContent);
                    //  Debug.WriteLine(responseContent);

                    //     Debug.WriteLine(responseContent);
                    // From here on you could deserialize the ResponseContent back again to a concrete C# type using Json.Net
                    return response;
                }
                return null;
            }
        }
    }
}
