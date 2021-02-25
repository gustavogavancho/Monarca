using System.Collections.Generic;

namespace Monarca.Tools.API.Models
{
    public class DocumentoBoleta
    {
        public string external_id { get; set; }
        public string motivo_anulacion { get; set; }
    }

    public class BajaBoletaRequest
    {
        public string fecha_de_emision_de_documentos { get; set; }
        public string codigo_tipo_proceso { get; set; }
        public List<DocumentoBoleta> documentos { get; set; }
    }
}
