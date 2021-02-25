using System;

namespace Monarca.COMMON.Entidades
{
    public class GastoOperativo : BaseEntity
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime? Fecha { get; set; }
        public decimal Costo { get; set; }
        public decimal SumaGastos { get; set; }
    }
}
