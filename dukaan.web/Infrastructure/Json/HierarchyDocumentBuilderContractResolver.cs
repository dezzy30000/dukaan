using dukaan.web.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Reflection;

namespace dukaan.web.Infrastructure.Json
{
    public class HierarchyDocumentBuilderContractResolver : DefaultContractResolver
    {
        private readonly string[] _propertiesToSerialize;

        public HierarchyDocumentBuilderContractResolver()
            : base()
        {
            var node = new Node();

            _propertiesToSerialize = new[] 
            {
                nameof(node.Id),
                nameof(node.Parent),
                nameof(node.Children)
            };
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (property.DeclaringType == typeof(Node))
            {
                if (_propertiesToSerialize.Any(propertyName => propertyName == property.PropertyName))
                {
                    property.ShouldSerialize = p => true;
                }
                else
                {
                    property.ShouldSerialize = p => false;
                }
            }

            return property;
        }
    }
}
