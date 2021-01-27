using Monarca.COMMON.Enumeraciones;

namespace Monarca.COMMON.Entidades
{
    public class Proveedor : BaseEntity
    {
        public TipoCliente TipoCliente { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string RazonSocial { get; set; }
        public string RepresentanteLegal { get; set; }
        public long Dni { get; set; }
        public long Ruc { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public long Celular { get; set; }
    }
}
