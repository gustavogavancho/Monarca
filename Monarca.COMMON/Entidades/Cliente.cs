namespace Monarca.COMMON.Entidades
{
    public class Cliente : BaseEntity
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string RazonSocial { get; set; }
        public string RepresentanteLegal { get; set; }
        public long Dni { get; set; }
        public long Ruc { get; set; }
        public string Dirección { get; set; }
        public string Email { get; set; }
    }
}
