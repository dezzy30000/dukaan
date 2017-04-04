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

        public IEnumerable<Node> Descendants
        {
            get { return Children.Concat(Children.SelectMany(child => child.Descendants)); }
        }

        public IEnumerable<Node> Ancestors
        {
            get
            {
                var parent = Parent;

                while (parent != null)
                {
                    yield return parent;
                    parent = parent.Parent;
                }
            }
        }


        public IEnumerable<Node> NodeAndDescendants
        {
            get { return new[] { this }.Concat(Descendants); }
        }

        public IEnumerable<Node> NodeAndAncestors
        {
            get
            {
                yield return this;

                foreach (var ancestor in Ancestors)
                {
                    yield return ancestor;
                }
            }
        }

        public PathString Path
        {
            get
            {
                var path = new PathString("/");

                foreach (var ancestor in NodeAndAncestors
                    .Reverse()
                    .Select(node => node.Slug))
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
                if (IsRoot || !ValidateAndExtractValueForSlugGeneration("Title", out string value))
                {
                    return new PathString("/");
                }

                return FromSlugToPathString(_slughelper.GenerateSlug(value));
            }
        }

        public PathString FromSlugToPathString(string slug)
        {
            return new PathString($"/{slug}");
        }

        private bool ValidateAndExtractValueForSlugGeneration(string slugContentPropertyName, out string value)
        {
            if (Content.TryGetValue(slugContentPropertyName, out JToken token) && token.Value<string>() != null)
            {
                value = token.Value<string>();
                return true;
            }

            var propertyValueExtractionIssue = token == null
                ? $"there is no property \"{slugContentPropertyName}\" on the {nameof(Content)} object."
                : $"the value of the property \"{slugContentPropertyName}\" on the {nameof(Content)} object is null.";

            //TODO:Change this to log to a logger and not throw and exception.
            propertyValueExtractionIssue = $"Cannot extract value to generate slug from document with id {Id} because {propertyValueExtractionIssue}";

            value = token.Value<string>();
            return false;
        }
    }
}
