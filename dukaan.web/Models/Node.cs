using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Slugify;
using System;
using System.Collections.Generic;
using System.Linq;

namespace dukaan.web.Models
{
    [JsonObject(IsReference = true)]
    public class Node
    {
        private const string SlugContentPropertyName = "Title";

        private readonly SlugHelper _slughelper = new SlugHelper();

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

        public bool IsRoot
        {
            get { return Parent == null; }
        }
        
        public IEnumerable<Node> NodeAndDescendants
        {
            get { return new[] { this }.Concat(Children.SelectMany(child => child.NodeAndDescendants)); }
        }

        public IEnumerable<Node> NodeAndAncestors
        {
            get
            {
                yield return this;

                var parent = Parent;

                while (parent != null)
                {
                    yield return parent;
                    parent = parent.Parent;
                }
            }
        }

        public PathString Path
        {
            get
            {
                var path = new PathString();

                foreach (var ancestor in NodeAndAncestors
                    .Reverse()
                    .Select(node => new PathString(node.Slug)))
                {
                    path = path.Add(ancestor);
                }

                return path;
            }
        }

        public PathString Slug
        {
            get
            {
                return IsRoot
                    ? new PathString("/")       
                    : FromSlugToPathString(_slughelper.GenerateSlug(ValidateAndExtractValueForSlugGeneration()));
            }
        }

        public PathString FromSlugToPathString(string slug)
        {
            return new PathString($"/{slug}");
        }

        private string ValidateAndExtractValueForSlugGeneration()
        {
            if (!Content.TryGetValue(SlugContentPropertyName, out JToken token) || string.IsNullOrWhiteSpace(token.Value<string>()))
            {
                var propertyValueExtractionIssue = token == null 
                    ? $"there is no property \"{SlugContentPropertyName}\" on the {nameof(Content)} object."
                    : $"the value of the property \"{SlugContentPropertyName}\" on the {nameof(Content)} object is empty. Value - {token.Value<string>()}.";

                throw new InvalidOperationException($"Cannot extract value to generate slug from document with id {Id} because {propertyValueExtractionIssue}");
            }

            return token.Value<string>();
        }
    }
}
