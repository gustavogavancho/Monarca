using System.Collections.Generic;

namespace Monarca.Tools.API.Models
{
    public class DocumentoFactura
    {
        public string external_id { get; set; }
        public string motivo_anulacion { get; set; }
    }

    public class BajaFacturaRequest
    {
        public string fecha_de_emision_de_documentos { get; set; }
        public List<DocumentoFactura> documentos { get; set; }
    }
}
