using dukaan.web.Infrastructure.Routing;
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
                if (IsRoot)
                {
                    return new PathString("/");
                }

                return _slughelper.GenerateSlug(Content.Value<string>("Title")).ToPathString();
            }
        }
    }
}
