using System.ComponentModel;

namespace Monarca.COMMON.Enumeraciones
{
    public enum TipoCliente : short
    {
        [Description("Persona Natural")]
        PersonaNatural = 1,
        [Description("Persona Jurídica")]
        PersonaJuridica = 2,
    }
}
