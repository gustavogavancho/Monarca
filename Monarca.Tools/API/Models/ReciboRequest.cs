using System.Collections.Generic;

namespace Monarca.Tools.API.Models
{
    public class DatosDelEmisor
    {
        public string codigo_pais { get; set; }
        public string ubigeo { get; set; }
        public string direccion { get; set; }
        public string correo_electronico { get; set; }
        public string telefono { get; set; }
        public string codigo_del_domicilio_fiscal { get; set; }
    }

    public class DatosDelClienteOReceptor
    {
        public string codigo_tipo_documento_identidad { get; set; }
        public string numero_documento { get; set; }
        public string apellidos_y_nombres_o_razon_social { get; set; }
        public string codigo_pais { get; set; }
        public string ubigeo { get; set; }
        public string direccion { get; set; }
        public string correo_electronico { get; set; }
        public string telefono { get; set; }
    }

    public class Totales
    {
        public int total_exportacion { get; set; }
        public int total_operaciones_gravadas { get; set; }
        public int total_operaciones_inafectas { get; set; }
        public string total_operaciones_exoneradas { get; set; }
        public int total_operaciones_gratuitas { get; set; }
        public int total_igv { get; set; }
        public int total_impuestos { get; set; }
        public string total_valor { get; set; }
        public string total_venta { get; set; }
    }

    public class Item
    {
        public string codigo_interno { get; set; }
        public string descripcion { get; set; }
        public string codigo_producto_sunat { get; set; }
        public string unidad_de_medida { get; set; }
        public string cantidad { get; set; }
        public string valor_unitario { get; set; }
        public string codigo_tipo_precio { get; set; }
        public string precio_unitario { get; set; }
        public string codigo_tipo_afectacion_igv { get; set; }
        public int total_base_igv { get; set; }
        public int porcentaje_igv { get; set; }
        public int total_igv { get; set; }
        public int total_impuestos { get; set; }
        public int total_valor_item { get; set; }
        public int total_item { get; set; }
    }

    public class ReciboRequest
    {
        public string serie_documento { get; set; }
        public string numero_documento { get; set; }
        public string fecha_de_emision { get; set; }
        public string hora_de_emision { get; set; }
        public string codigo_tipo_operacion { get; set; }
        public string codigo_tipo_documento { get; set; }
        public string codigo_tipo_moneda { get; set; }
        public string fecha_de_vencimiento { get; set; }
        public string numero_orden_de_compra { get; set; }
        public DatosDelEmisor datos_del_emisor { get; set; }
        public DatosDelClienteOReceptor datos_del_cliente_o_receptor { get; set; }
        public Totales totales { get; set; }
        public List<Item> items { get; set; }
        public string informacion_adicional { get; set; }
    }

}
