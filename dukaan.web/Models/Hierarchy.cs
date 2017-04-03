using Newtonsoft.Json;
using System;
using System.Linq;

namespace dukaan.web.Models
{
    [JsonObject(IsReference = false)]
    public class Hierarchy
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public Node Root { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Node GetNodeBySlug(string slug)
        {
            return Root
                .NodeAndDescendants
                .SingleOrDefault(node => node.Slug.Equals(node.FromSlugToPathString(slug)));
        }
    }
}
