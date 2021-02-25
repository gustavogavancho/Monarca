namespace Monarca.Tools.API.Models
{
    using System.Collections.Generic;

    namespace APIFacturadorSunat.Model
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

        public class Response
        {
            public string code { get; set; }
            public string description { get; set; }
            public List<string> notes { get; set; }
        }

        public class FacturaReponse
        {
            public bool success { get; set; }
            public string message { get; set; }
            public Data data { get; set; }
            public Links links { get; set; }
            public Response response { get; set; }
        }
    }

}
