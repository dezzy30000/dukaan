using dukaan.web.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using Xunit;

namespace dukaan.web.tests.Models
{
    public class NodeTests
    {
        [Fact]
        public void GivenANodeHierarchyWhenAllDecendantsOfANodeAreRequiredFromTheRootThenAllNodesIncludingSelfAreCorrectlyReturned()
        {
            var root = BuildHierarchy();

            var nodeAndDecendants = root.NodeAndDescendants;

            Assert.Equal(18, nodeAndDecendants.Count());

            Assert.Equal("1", nodeAndDecendants.ElementAt(0).Id);
            Assert.Equal("2", nodeAndDecendants.ElementAt(1).Id);
            Assert.Equal("3", nodeAndDecendants.ElementAt(2).Id);
            Assert.Equal("4", nodeAndDecendants.ElementAt(3).Id);
            Assert.Equal("5", nodeAndDecendants.ElementAt(4).Id);
            Assert.Equal("6", nodeAndDecendants.ElementAt(5).Id);
            Assert.Equal("7", nodeAndDecendants.ElementAt(6).Id);
            Assert.Equal("8", nodeAndDecendants.ElementAt(7).Id);
            Assert.Equal("9", nodeAndDecendants.ElementAt(8).Id);
            Assert.Equal("10", nodeAndDecendants.ElementAt(9).Id);
            Assert.Equal("11", nodeAndDecendants.ElementAt(10).Id);
            Assert.Equal("12", nodeAndDecendants.ElementAt(11).Id);
            Assert.Equal("13", nodeAndDecendants.ElementAt(12).Id);
            Assert.Equal("14", nodeAndDecendants.ElementAt(13).Id);
            Assert.Equal("15", nodeAndDecendants.ElementAt(14).Id);
            Assert.Equal("16", nodeAndDecendants.ElementAt(15).Id);
            Assert.Equal("17", nodeAndDecendants.ElementAt(16).Id);
            Assert.Equal("18", nodeAndDecendants.ElementAt(17).Id);
        }

        [Fact]
        public void GivenANodeHierarchyWhenAllDecendantsOfANodeAreRequiredFromALeafThenAllNodesIncludingSelfAreCorrectlyReturned()
        {
            var leaf = BuildHierarchy()
                .Children
                .First()
                .Children
                .First();

            var nodeAndDecendants = leaf.NodeAndDescendants;

            Assert.Equal(1, nodeAndDecendants.Count());

            Assert.Equal("5", nodeAndDecendants.ElementAt(0).Id);
        }

        [Fact]
        public void GivenANodeHierarchyWhenAllDecendantsOfANodeAreRequiredFromAMidLevelNodeThenAllNodesIncludingSelfAreCorrectlyReturned()
        {
            var midLevelNode = BuildHierarchy()
                .Children
                .ElementAt(1);

            var nodeAndDecendants = midLevelNode.NodeAndDescendants;

            Assert.Equal(4, nodeAndDecendants.Count());

            Assert.Equal("3", nodeAndDecendants.ElementAt(0).Id);
            Assert.Equal("9", nodeAndDecendants.ElementAt(1).Id);
            Assert.Equal("10", nodeAndDecendants.ElementAt(2).Id);
            Assert.Equal("11", nodeAndDecendants.ElementAt(3).Id);
        }

        [Fact]
        public void GivenANodeHierarchyWhenAllAncestorsOfANodeAreRequiredFromALeafThenAllNodesIncludingSelfAreCorrectlyReturned()
        {
            var leaf = BuildHierarchy()
                .Children
                .ElementAt(1)
                .Children
                .First()
                .Children
                .First()
                .Children
                .First();

            var nodeAndAncestors = leaf.NodeAndAncestors;

            Assert.Equal(5, nodeAndAncestors.Count());

            Assert.Equal("11", nodeAndAncestors.ElementAt(0).Id);
            Assert.Equal("10", nodeAndAncestors.ElementAt(1).Id);
            Assert.Equal("9", nodeAndAncestors.ElementAt(2).Id);
            Assert.Equal("3", nodeAndAncestors.ElementAt(3).Id);
            Assert.Equal("1", nodeAndAncestors.ElementAt(4).Id);
        }

        [Fact]
        public void GivenANodeHierarchyWhenAllAncestorsOfANodeAreRequiredFromARootThenAllNodesIncludingSelfAreCorrectlyReturned()
        {
            var root = BuildHierarchy();

            var nodeAndAncestors = root.NodeAndAncestors;

            Assert.Equal(1, nodeAndAncestors.Count());

            Assert.Equal("1", nodeAndAncestors.ElementAt(0).Id);
        }

        [Fact]
        public void GivenANodeHierarchyWhenAllAncestorsOfANodeAreRequiredFromAMidLevelNodeThenAllNodesIncludingSelfAreCorrectlyReturned()
        {
            var midLevelNode = BuildHierarchy()
                .Children
                .ElementAt(2)
                .Children
                .First();

            var nodeAndAncestors = midLevelNode.NodeAndAncestors;

            Assert.Equal(3, nodeAndAncestors.Count());

            Assert.Equal("12", nodeAndAncestors.ElementAt(0).Id);
            Assert.Equal("4", nodeAndAncestors.ElementAt(1).Id);
            Assert.Equal("1", nodeAndAncestors.ElementAt(2).Id);
        }

        [Fact]
        public void GivenANodeHierarchyWhenAllDecendantsOfANodeAreRequiredFromTheRootThenAllNodesAreCorrectlyReturned()
        {
            var root = BuildHierarchy();

            var decendants = root.Descendants;

            Assert.Equal(17, decendants.Count());

            Assert.Equal("2", decendants.ElementAt(0).Id);
            Assert.Equal("3", decendants.ElementAt(1).Id);
            Assert.Equal("4", decendants.ElementAt(2).Id);
            Assert.Equal("5", decendants.ElementAt(3).Id);
            Assert.Equal("6", decendants.ElementAt(4).Id);
            Assert.Equal("7", decendants.ElementAt(5).Id);
            Assert.Equal("8", decendants.ElementAt(6).Id);
            Assert.Equal("9", decendants.ElementAt(7).Id);
            Assert.Equal("10", decendants.ElementAt(8).Id);
            Assert.Equal("11", decendants.ElementAt(9).Id);
            Assert.Equal("12", decendants.ElementAt(10).Id);
            Assert.Equal("13", decendants.ElementAt(11).Id);
            Assert.Equal("14", decendants.ElementAt(12).Id);
            Assert.Equal("15", decendants.ElementAt(13).Id);
            Assert.Equal("16", decendants.ElementAt(14).Id);
            Assert.Equal("17", decendants.ElementAt(15).Id);
            Assert.Equal("18", decendants.ElementAt(16).Id);
        }

        [Fact]
        public void GivenANodeHierarchyWhenAllDecendantsOfANodeAreRequiredFromALeafThenAllNodesAreCorrectlyReturned()
        {
            var leaf = BuildHierarchy()
                .Children
                .First()
                .Children
                .First();
            
            Assert.Equal(0, leaf.Descendants.Count());
        }

        [Fact]
        public void GivenANodeHierarchyWhenAllDecendantsOfANodeAreRequiredFromAMidLevelNodeThenAllNodesAreCorrectlyReturned()
        {
            var midLevelNode = BuildHierarchy()
                .Children
                .ElementAt(1);

            var decendants = midLevelNode.Descendants;

            Assert.Equal(3, decendants.Count());

            Assert.Equal("9", decendants.ElementAt(0).Id);
            Assert.Equal("10", decendants.ElementAt(1).Id);
            Assert.Equal("11", decendants.ElementAt(2).Id);
        }

        [Fact]
        public void GivenANodeHierarchyWhenAllAncestorsOfANodeAreRequiredFromALeafThenAllNodesAreCorrectlyReturned()
        {
            var leaf = BuildHierarchy()
                .Children
                .ElementAt(1)
                .Children
                .First()
                .Children
                .First()
                .Children
                .First();

            var ancestors = leaf.Ancestors;

            Assert.Equal(4, ancestors.Count());

            Assert.Equal("10", ancestors.ElementAt(0).Id);
            Assert.Equal("9", ancestors.ElementAt(1).Id);
            Assert.Equal("3", ancestors.ElementAt(2).Id);
            Assert.Equal("1", ancestors.ElementAt(3).Id);
        }

        [Fact]
        public void GivenANodeHierarchyWhenAllAncestorsOfANodeAreRequiredFromARootThenAllNodeAreCorrectlyReturned()
        {
            var root = BuildHierarchy();

            Assert.Equal(0, root.Ancestors.Count());
        }

        [Fact]
        public void GivenANodeHierarchyWhenAllAncestorsOfANodeAreRequiredFromAMidLevelNodeThenAllNodesAreCorrectlyReturned()
        {
            var midLevelNode = BuildHierarchy()
                .Children
                .ElementAt(2)
                .Children
                .First();

            var ancestors = midLevelNode.Ancestors;

            Assert.Equal(2, ancestors.Count());

            Assert.Equal("4", ancestors.ElementAt(0).Id);
            Assert.Equal("1", ancestors.ElementAt(1).Id);
        }

        //[Fact]
        //public void GivenANodeHierarchyThenOnlyOneAndTopNotShouldBeTheRootNode()
        //{
        //    var root = BuildHierarchy();

        //    Assert.True(root.IsRoot);

        //    root. .NodeAndDescendants()
        //}

        #region Test data

        private Node BuildHierarchy()
        {
            var root = new Node
            {
                Id = "1",
                Content = JObject.Parse("{\"Title\":\"Aenean\"}"),
                Children = new[]
                {
                    new Node
                    {
                        Id = "2",
                        Content = JObject.Parse("{\"Title\":\"magna\"}"),
                        Children = new []
                        {
                            new Node { Id = "5", Content = JObject.Parse("{\"Title\":\"nunc\"}") },
                            new Node { Id = "6", Content = JObject.Parse("{\"Title\":\"Proin euismod laoreet tellus\"}") },
                            new Node { Id = "7", Content = JObject.Parse("{\"Title\":\"at\"}")},
                            new Node { Id = "8", Content = JObject.Parse("{\"Title\":\"velit\"}") },
                        }
                    },
                    new Node
                    {
                        Id = "3",
                        Content = JObject.Parse("{\"Title\":\"ornare gravida diam\"}"),
                        Children = new []
                        {
                            new Node
                            {
                                Id = "9",
                                Content = JObject.Parse("{\"Title\":\"quis consectetur erat\"}"),
                                Children = new []
                                {
                                    new Node
                                    {
                                        Id = "10",
                                        Content = JObject.Parse("{\"Title\":\"consequat egestas\"}"),
                                        Children = new []
                                        {
                                            new Node
                                            {
                                                Id = "11",
                                                Content = JObject.Parse("{\"Title\":\"Quisque\"}")
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    },
                    new Node
                    {
                        Id = "4",
                        Content = JObject.Parse("{\"Title\":\"convallis\"}"),
                        Children = new []
                        {
                            new Node
                            {
                                Id = "12",
                                Content = JObject.Parse("{\"Title\":\"Vivamus\"}"),
                                Children = new []
                                {
                                    new Node { Id = "13", Content = JObject.Parse("{\"Title\":\"hendrerit\"}") },
                                    new Node { Id = "14", Content = JObject.Parse("{\"Title\":\"tincidunt massa\"}") },
                                    new Node { Id = "15", Content = JObject.Parse("{\"Title\":\"Donec a\"}") },
                                    new Node { Id = "16", Content = JObject.Parse("{\"Title\":\"quam at facilisis\"}") },
                                    new Node { Id = "17", Content = JObject.Parse("{\"Title\":\"Suspendisse\"}") },
                                    new Node { Id = "18", Content = JObject.Parse("{\"Title\":\"metus at lacus\"}") },
                                }
                            }
                        }
                    },
                }
            };


            TraverseWithParent(root, null, (node, parent) =>
            {
                node.Parent = parent;
            });

            return root;
        }

        private void TraverseWithParent(Node node, Node parent, Action<Node, Node> callback)
        {
            for (int index = 0; index < node.Children.Length; index++)
            {
                TraverseWithParent(node.Children[index], node, callback);
            }

            callback(node, parent);
        }

        #endregion
    }
}
