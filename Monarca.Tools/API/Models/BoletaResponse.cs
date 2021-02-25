using System.Collections.Generic;

namespace Monarca.Tools.API.Models
{
    public class Data
    {
        public string number { get; set; }
        public string filename { get; set; }
        public string external_id { get; set; }
        public string number_to_letter { get; set; }
        public string hash { get; set; }
        public string qr { get; set; }
    }

    public class Links
    {
        public string xml { get; set; }
        public string pdf { get; set; }
        public string cdr { get; set; }
    }

    public class BoletaResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
        public Links links { get; set; }
        public List<object> response { get; set; }
    }
}
