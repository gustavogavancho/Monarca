namespace Monarca.Tools.API.Models
{
    public class DataBaja
    {
        public string external_id { get; set; }
        public string ticket { get; set; }
    }

    public class BajaResponse
    {
        public bool success { get; set; }
        public DataBaja data { get; set; }
    }
}
