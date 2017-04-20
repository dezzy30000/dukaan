using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace dukaan.web.Models
{
    public class ShoppingCartItems
    {
        [JsonProperty(PropertyName = "quantity")]
        public int Quantity { get; set; }
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "price")]
        public decimal Price { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "nodeid")]
        public string NodeId { get; set; }
        [JsonProperty(PropertyName = "image")]
        public string Image { get; set; }
    }
}
