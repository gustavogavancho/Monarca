using Monarca.BIZ;
using Monarca.COMMON.Entidades;
using Monarca.COMMON.Enumeraciones;
using Monarca.COMMON.Interfaces;
using Monarca.Tools.API.Models;
using Monarca.Tools.API.Models.APIFacturadorSunat.Model;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Monarca.Tools.API
{
    public class ConsultaEmitirRecibo
    {
        static FactoryManager _factoryManager = new FactoryManager("LiteDB");
        static IClienteManager _clienteManager = _factoryManager.CrearClienteManager;

        public static async Task<BoletaResponse> Envio_seguro_boleta(Venta venta)
        {
            Cliente cliente = _clienteManager.SearchById(venta.IdCliente);
            string url = $"https://facturacion.selvafood.com/api/documents";

            int tipoDocumento = (int)venta.TipoVenta;
            ReciboRequest body = new ReciboRequest
            {
                serie_documento = venta.SerieDocumento,
                numero_documento = venta.NumeroDocumento,
                fecha_de_emision = DateTime.Now.Date.ToString("yyyy-MM-dd"),
                hora_de_emision = DateTime.Now.ToLongTimeString(),
                codigo_tipo_operacion = "0101",
                codigo_tipo_documento = tipoDocumento.ToString("D2"),
                codigo_tipo_moneda = "PEN",
                fecha_de_vencimiento = DateTime.Now.Date.ToString("yyyy-MM-dd"),
                numero_orden_de_compra = venta.NumeroDocumento,
                datos_del_emisor = new DatosDelEmisor
                {
                    codigo_pais = "PE",
                    ubigeo = "220901",
                    direccion = "Psje. Limatambo 121, Tarapoto",
                    telefono = "916374928",
                    codigo_del_domicilio_fiscal = "0000"
                },
                datos_del_cliente_o_receptor = new DatosDelClienteOReceptor
                {
                    codigo_tipo_documento_identidad = ((int)cliente.TipoCliente).ToString(),
                    numero_documento = (cliente.TipoCliente == TipoCliente.PersonaNatural) ? venta.Dni.ToString() : venta.Ruc.ToString(),
                    apellidos_y_nombres_o_razon_social = (cliente.TipoCliente == TipoCliente.PersonaNatural) ? venta.NombreProveedor : venta.RazonSocialProveedor,
                    codigo_pais = "PE",
                    ubigeo = "220901",
                    correo_electronico = cliente.Email,
                    telefono = cliente.Celular.ToString(),
                },
                totales = new Totales
                {
                    total_exportacion = 0,
                    total_operaciones_gravadas = 0,
                    total_operaciones_inafectas = 0,
                    total_operaciones_exoneradas = venta.Productos.Sum(x=> x.Total).ToString("0.##"),
                    total_operaciones_gratuitas = 0,
                    total_igv = 0,
                    total_impuestos = 0,
                    total_valor = venta.Productos.Sum(x => x.Total).ToString("0.##"),
                    total_venta = venta.Productos.Sum(x => x.Total).ToString("0.##"),

                },
                items = new System.Collections.Generic.List<Item>(),
                informacion_adicional = "OBSERVACI\u00d3N: ",
            };
            foreach (var item in venta.Productos)
            {
                body.items.Add(new Item
                {
                    codigo_interno = item.CodigoInterno,
                    descripcion = item.Descripción,
                    codigo_producto_sunat = "51121703",
                    unidad_de_medida = item.Unidad.ToString(),
                    cantidad = item.Cantidad.ToString("0.##"),
                    valor_unitario = item.PrecioUnitario.ToString("0.##"),
                    codigo_tipo_precio = "01",
                    precio_unitario = item.PrecioUnitario.ToString("0.##"),
                    codigo_tipo_afectacion_igv = "20",
                    total_base_igv = 2,
                    porcentaje_igv = 18,
                    total_igv = 0,
                    total_impuestos = 0,
                    total_valor_item = 2,
                    total_item = 2
                });
            }

            string json = JsonConvert.SerializeObject(body);

            dynamic jsoncheck = JsonConvert.DeserializeObject(json);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            String responseContent = "";
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("authorization", "Bearer xtu5401IsF1KEH5IsDUNk5CrNhqdsUEArEcAUBGG5Q8y27E9Mn");
                // Do the actual request and await the response
                var httpResponse = await httpClient.PostAsync(url, httpContent);

                // If the response contains content we want to read it!
                if (httpResponse.Content != null)
                {
                    responseContent = await httpResponse.Content.ReadAsStringAsync();

                    var response = JsonConvert.DeserializeObject<BoletaResponse>(responseContent);
                    //  Debug.WriteLine(responseContent);

                    //     Debug.WriteLine(responseContent);
                    // From here on you could deserialize the ResponseContent back again to a concrete C# type using Json.Net
                    return response;
                }
                return null;
            }
        }

        public static async Task<FacturaReponse> Envio_seguro_factura(Venta venta)
        {
            Cliente cliente = _clienteManager.SearchById(venta.IdCliente);
            string url = $"https://facturacion.selvafood.com/api/documents";

            int tipoDocumento = (int)venta.TipoVenta;
            ReciboRequest body = new ReciboRequest
            {
                serie_documento = venta.SerieDocumento,
                numero_documento = venta.NumeroDocumento,
                fecha_de_emision = DateTime.Now.Date.ToString("yyyy-MM-dd"),
                hora_de_emision = DateTime.Now.ToLongTimeString(),
                codigo_tipo_operacion = "0101",
                codigo_tipo_documento = tipoDocumento.ToString("D2"),
                codigo_tipo_moneda = "PEN",
                fecha_de_vencimiento = DateTime.Now.Date.ToString("yyyy-MM-dd"),
                numero_orden_de_compra = venta.NumeroDocumento,
                datos_del_emisor = new DatosDelEmisor
                {
                    codigo_pais = "PE",
                    ubigeo = "220901",
                    direccion = "Psje. Limatambo 121, Tarapoto",
                    telefono = "916374928",
                    codigo_del_domicilio_fiscal = "0000"
                },
                datos_del_cliente_o_receptor = new DatosDelClienteOReceptor
                {
                    codigo_tipo_documento_identidad = ((int)cliente.TipoCliente).ToString(),
                    numero_documento = (cliente.TipoCliente == TipoCliente.PersonaNatural) ? venta.Dni.ToString() : venta.Ruc.ToString(),
                    apellidos_y_nombres_o_razon_social = (cliente.TipoCliente == TipoCliente.PersonaNatural) ? venta.NombreProveedor : venta.RazonSocialProveedor,
                    codigo_pais = "PE",
                    ubigeo = "220901",
                    correo_electronico = cliente.Email,
                    telefono = cliente.Celular.ToString(),
                },
                totales = new Totales
                {
                    total_exportacion = 0,
                    total_operaciones_gravadas = 0,
                    total_operaciones_inafectas = 0,
                    total_operaciones_exoneradas = venta.Productos.Sum(x => x.Total).ToString("0.##"),
                    total_operaciones_gratuitas = 0,
                    total_igv = 0,
                    total_impuestos = 0,
                    total_valor = venta.Productos.Sum(x => x.Total).ToString("0.##"),
                    total_venta = venta.Productos.Sum(x => x.Total).ToString("0.##"),

                },
                items = new System.Collections.Generic.List<Item>(),
                informacion_adicional = "OBSERVACI\u00d3N: ",
            };
            foreach (var item in venta.Productos)
            {
                body.items.Add(new Item
                {
                    codigo_interno = item.CodigoInterno,
                    descripcion = item.Descripción,
                    codigo_producto_sunat = "51121703",
                    unidad_de_medida = item.Unidad.ToString(),
                    cantidad = item.Cantidad.ToString("0.##"),
                    valor_unitario = item.PrecioUnitario.ToString("0.##"),
                    codigo_tipo_precio = "01",
                    precio_unitario = item.PrecioUnitario.ToString("0.##"),
                    codigo_tipo_afectacion_igv = "20",
                    total_base_igv = 2,
                    porcentaje_igv = 18,
                    total_igv = 0,
                    total_impuestos = 0,
                    total_valor_item = 2,
                    total_item = 2
                });
            }

            string json = JsonConvert.SerializeObject(body);

            dynamic jsoncheck = JsonConvert.DeserializeObject(json);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");


            String responseContent = "";
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("authorization", "Bearer xtu5401IsF1KEH5IsDUNk5CrNhqdsUEArEcAUBGG5Q8y27E9Mn");
                // Do the actual request and await the response
                var httpResponse = await httpClient.PostAsync(url, httpContent);

                // If the response contains content we want to read it!
                if (httpResponse.Content != null)
                {
                    responseContent = await httpResponse.Content.ReadAsStringAsync();

                    var response = JsonConvert.DeserializeObject<FacturaReponse>(responseContent);
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
