using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace dukaan.web.Models
{
    [JsonObject(IsReference = false)]
    public class Hierarchy
    {
        [JsonObject(IsReference = true)]
        public class Node
        {
            public Node()
            {
                Children = new Node[] { };
            }

            public string Id { get; set; }
            public string Type { get; set; }
            public JObject Content { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }
            public Node Parent { get; set; }
            public Node[] Children { get; set; }
        }

        public string Id { get; set; }
        public string Key { get; set; }
        public Node Root { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
