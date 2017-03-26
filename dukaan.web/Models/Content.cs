using Newtonsoft.Json;

namespace dukaan.web.Models
{
    [JsonObject(IsReference = false)]
    public class Content
    {
        public string Name { get; set; }
        public string Body { get; set; }
    }
}
