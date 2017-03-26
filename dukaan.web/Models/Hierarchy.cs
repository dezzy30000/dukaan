using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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
                Content = new Content();
                Children = new List<Node>();
            }

            public string Id { get; set; }
            public string Type { get; set; }
            public Content Content { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }
            public Node Parent { get; set; }
            public List<Node> Children { get; set; }
        }

        public string Id { get; set; }
        public string Key { get; set; }
        public Node Root { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
