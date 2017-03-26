using dukaan.web.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xunit;
using static dukaan.web.Models.Hierarchy;

namespace dukaan.dataapi.tests
{
    public class DocumentHierarchyBuilderTests
    {
        [Fact]
        public void GivenAHierarchyStructureWhenSerialisedUsingTheSerilaisationModelsThenADocumentHierarchyJsonStringIsCorrectlyBuilt()
        {
            var expected = "{\"$id\":\"1\",\"Id\":\"1472459628771017730\",\"Parent\":null,\"Children\":[{\"$id\":\"2\",\"Id\":\"1472459628812960771\",\"Parent\":{\"$ref\":\"1\"},\"Children\":[{\"$id\":\"3\",\"Id\":\"1472459628812960772\",\"Parent\":{\"$ref\":\"2\"},\"Children\":[{\"$id\":\"4\",\"Id\":\"1472459628812960773\",\"Parent\":{\"$ref\":\"3\"},\"Children\":[{\"$id\":\"5\",\"Id\":\"1472459628812960774\",\"Parent\":{\"$ref\":\"4\"},\"Children\":[{\"$id\":\"6\",\"Id\":\"1472459628829738023\",\"Parent\":{\"$ref\":\"5\"},\"Children\":[{\"$id\":\"7\",\"Id\":\"1472459628829738024\",\"Parent\":{\"$ref\":\"6\"},\"Children\":[{\"$id\":\"8\",\"Id\":\"1472459628829738025\",\"Parent\":{\"$ref\":\"7\"},\"Children\":[]}]}]}]},{\"$id\":\"9\",\"Id\":\"1472459628812960775\",\"Parent\":{\"$ref\":\"4\"},\"Children\":[]},{\"$id\":\"10\",\"Id\":\"1472459628812960776\",\"Parent\":{\"$ref\":\"4\"},\"Children\":[]}]}]},{\"$id\":\"11\",\"Id\":\"1472459628812960777\",\"Parent\":{\"$ref\":\"2\"},\"Children\":[]}]},{\"$id\":\"12\",\"Id\":\"1472459628812960778\",\"Parent\":{\"$ref\":\"1\"},\"Children\":[{\"$id\":\"13\",\"Id\":\"1472459628812960779\",\"Parent\":{\"$ref\":\"12\"},\"Children\":[{\"$id\":\"14\",\"Id\":\"1472459628812960780\",\"Parent\":{\"$ref\":\"13\"},\"Children\":[]},{\"$id\":\"15\",\"Id\":\"1472459628812960781\",\"Parent\":{\"$ref\":\"13\"},\"Children\":[]},{\"$id\":\"16\",\"Id\":\"1472459628812960782\",\"Parent\":{\"$ref\":\"13\"},\"Children\":[]},{\"$id\":\"17\",\"Id\":\"1472459628812960783\",\"Parent\":{\"$ref\":\"13\"},\"Children\":[]},{\"$id\":\"18\",\"Id\":\"1472459628821349392\",\"Parent\":{\"$ref\":\"13\"},\"Children\":[]},{\"$id\":\"19\",\"Id\":\"1472459628821349393\",\"Parent\":{\"$ref\":\"13\"},\"Children\":[]}]}]},{\"$id\":\"20\",\"Id\":\"1472459628821349394\",\"Parent\":{\"$ref\":\"1\"},\"Children\":[{\"$id\":\"21\",\"Id\":\"1472459628821349395\",\"Parent\":{\"$ref\":\"20\"},\"Children\":[{\"$id\":\"22\",\"Id\":\"1472459628821349396\",\"Parent\":{\"$ref\":\"21\"},\"Children\":[{\"$id\":\"23\",\"Id\":\"1472459628821349397\",\"Parent\":{\"$ref\":\"22\"},\"Children\":[]},{\"$id\":\"24\",\"Id\":\"1472459628821349398\",\"Parent\":{\"$ref\":\"22\"},\"Children\":[]},{\"$id\":\"25\",\"Id\":\"1472459628821349399\",\"Parent\":{\"$ref\":\"22\"},\"Children\":[]},{\"$id\":\"26\",\"Id\":\"1472459628821349400\",\"Parent\":{\"$ref\":\"22\"},\"Children\":[]},{\"$id\":\"27\",\"Id\":\"1472459628821349401\",\"Parent\":{\"$ref\":\"22\"},\"Children\":[]},{\"$id\":\"28\",\"Id\":\"1472459628821349402\",\"Parent\":{\"$ref\":\"22\"},\"Children\":[]}]}]}]},{\"$id\":\"29\",\"Id\":\"1472459628821349499\",\"Parent\":{\"$ref\":\"1\"},\"Children\":[{\"$id\":\"30\",\"Id\":\"1472459628821349403\",\"Parent\":{\"$ref\":\"29\"},\"Children\":[]},{\"$id\":\"31\",\"Id\":\"1472459628821349404\",\"Parent\":{\"$ref\":\"29\"},\"Children\":[]},{\"$id\":\"32\",\"Id\":\"1472459628821349405\",\"Parent\":{\"$ref\":\"29\"},\"Children\":[]},{\"$id\":\"33\",\"Id\":\"1472459628821349406\",\"Parent\":{\"$ref\":\"29\"},\"Children\":[]},{\"$id\":\"34\",\"Id\":\"1472459628829738015\",\"Parent\":{\"$ref\":\"29\"},\"Children\":[]},{\"$id\":\"35\",\"Id\":\"1472459628829738016\",\"Parent\":{\"$ref\":\"29\"},\"Children\":[]},{\"$id\":\"36\",\"Id\":\"1472459628829738017\",\"Parent\":{\"$ref\":\"29\"},\"Children\":[]},{\"$id\":\"37\",\"Id\":\"1472459628829738018\",\"Parent\":{\"$ref\":\"29\"},\"Children\":[]},{\"$id\":\"38\",\"Id\":\"1472459628829738019\",\"Parent\":{\"$ref\":\"29\"},\"Children\":[]},{\"$id\":\"39\",\"Id\":\"1472459628829738020\",\"Parent\":{\"$ref\":\"29\"},\"Children\":[]},{\"$id\":\"40\",\"Id\":\"1472459628829738021\",\"Parent\":{\"$ref\":\"29\"},\"Children\":[]},{\"$id\":\"41\",\"Id\":\"1472459628829738022\",\"Parent\":{\"$ref\":\"29\"},\"Children\":[]}]}]}";

            var actual = JsonConvert.SerializeObject(BuildHierarchy(), new JsonSerializerSettings
            {
                ContractResolver = new HierarchyDocumentBuilderContractResolver()
            });

            Assert.Equal(expected, actual);
        }

        #region Create hierarchy model

        private Node BuildHierarchy()
        {
            var root = new Node
            {
                Id = "1472459628771017730",
                Children = new List<Node>
                {
                    new Node
                    {
                        Id = "1472459628812960771",
                        Children = new List<Node>
                        {
                            new Node
                            {
                                Id = "1472459628812960772",
                                Children = new List<Node>
                                {
                                    new Node
                                    {
                                        Id = "1472459628812960773",
                                        Children = new List<Node>
                                        {
                                            new Node
                                            {
                                                Id = "1472459628812960774",
                                                Children = new List<Node>
                                                {
                                                    new Node
                                                    {
                                                        Id = "1472459628829738023",
                                                        Children = new List<Node>
                                                        {
                                                            new Node
                                                            {
                                                                Id = "1472459628829738024",
                                                                Children = new List<Node>
                                                                {
                                                                    new Node
                                                                    {
                                                                        Id = "1472459628829738025"
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            },
                                            new Node
                                            {
                                                Id = "1472459628812960775"
                                            },
                                            new Node
                                            {
                                                Id = "1472459628812960776"
                                            }
                                        }
                                    }
                                }
                            },
                            new Node
                            {
                                Id = "1472459628812960777"
                            }
                        }
                    },
                    new Node
                    {
                        Id = "1472459628812960778",
                        Children = new List<Node>
                        {
                            new Node
                            {
                                Id = "1472459628812960779",
                                Children = new List<Node>
                                {
                                    new Node { Id = "1472459628812960780" },
                                    new Node { Id = "1472459628812960781" },
                                    new Node { Id = "1472459628812960782" },
                                    new Node { Id = "1472459628812960783" },
                                    new Node { Id = "1472459628821349392" },
                                    new Node { Id = "1472459628821349393" }
                                }
                            }
                        }
                    },
                    new Node
                    {
                        Id = "1472459628821349394",
                        Children = new List<Node>
                        {
                            new Node
                            {
                                Id = "1472459628821349395",
                                Children = new List<Node>
                                {
                                    new Node
                                    {
                                        Id = "1472459628821349396",
                                        Children = new List<Node>
                                        {
                                            new Node { Id = "1472459628821349397" },
                                            new Node { Id = "1472459628821349398" },
                                            new Node { Id = "1472459628821349399" },
                                            new Node { Id = "1472459628821349400" },
                                            new Node { Id = "1472459628821349401" },
                                            new Node { Id = "1472459628821349402" }
                                        }
                                    }
                                }
                            }
                        }
                    },
                    new Node
                    {
                        Id = "1472459628821349499",
                        Children = new List<Node>
                        {
                            new Node { Id = "1472459628821349403" },
                            new Node { Id = "1472459628821349404" },
                            new Node { Id = "1472459628821349405" },
                            new Node { Id = "1472459628821349406" },
                            new Node { Id = "1472459628829738015" },
                            new Node { Id = "1472459628829738016" },
                            new Node { Id = "1472459628829738017" },
                            new Node { Id = "1472459628829738018" },
                            new Node { Id = "1472459628829738019" },
                            new Node { Id = "1472459628829738020" },
                            new Node { Id = "1472459628829738021" },
                            new Node { Id = "1472459628829738022" }
                        }
                    }
                }
            };

            Traverse(root, null, (node, parent) =>
            {
                node.Parent = parent;
            });

            return root;
        }

        private static void Traverse(Node node, Node parent, Action<Node, Node> callback)
        {
            for (int index = 0; index < node.Children.Count; index++)
            {
                Traverse(node.Children[index], node, callback);
            }

            callback(node, parent);
        }

        #endregion
    }
}
