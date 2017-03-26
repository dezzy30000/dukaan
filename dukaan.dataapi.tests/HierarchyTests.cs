using dukaan.web.Infrastructure;
using dukaan.web.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;
using static dukaan.web.Models.Hierarchy;

namespace dukaan.dataapi.tests
{
    public class HierarchyTests
    {
        [Fact]
        public void GivenAHierarchyLocateAllNodeIdsWhenParsedThenAListOfIdsIsReturned()
        {
            ConnectAndPrepareDatabase(command =>
            {
                var expected = new[]
                {
                    "1472459628829738025","1472459628829738024","1472459628829738023","1472459628812960774",
                    "1472459628812960775","1472459628812960776","1472459628812960773","1472459628812960772",
                    "1472459628812960777","1472459628812960771","1472459628812960780","1472459628812960781",
                    "1472459628812960782","1472459628812960783","1472459628821349392","1472459628821349393",
                    "1472459628812960779","1472459628812960778","1472459628821349397","1472459628821349398",
                    "1472459628821349399","1472459628821349400","1472459628821349401","1472459628821349402",
                    "1472459628821349396","1472459628821349395","1472459628821349394","1472459628821349403",
                    "1472459628821349404","1472459628821349405","1472459628821349406","1472459628829738015",
                    "1472459628829738016","1472459628829738017","1472459628829738018","1472459628829738019",
                    "1472459628829738020","1472459628829738021","1472459628829738022","1472459628821349499",
                    "1472459628771017730"
                };

                command
                    .CommandText = $"{command.CommandText};select get_hierarchy_ids('website');";

                var actual = (string[])command.ExecuteScalar();

                Assert.Equal(expected, actual);
            },
            @".\TestData\HierarchyTests.sql");
        }

        private void Traverse(JObject node, Action<JObject> asserts)
        {
            foreach (var child in ((JArray)node["Children"]))
            {
                Traverse((JObject)child, asserts);
            }

            asserts(node);
        }

        [Fact]
        public void GivenASkeletonHierarchyWhenHydratedThenTheHierarchyIsPopulatedWithNodeData()
        {
            var assertsNodeData = new[]
            {
                new { Id = "1472459628771017730", ReferenceId = "1", ParentReferenceId = (string)null, Type = "cras", Content = "{\"Body\":\"Lorem ipsum dolor sit amet, consectetur adipiscing elit.\",\"Name\":\"lorem\"}", ChildCount = 4 },
                new { Id = "1472459628812960771", ReferenceId = "2", ParentReferenceId = "1", Type = "morbi", Content = "{\"Body\":\"Suspendisse a arcu a tortor scelerisque auctor vitae et nisl.\",\"Name\":\"ipsum dolor\"}", ChildCount = 2 },
                new { Id = "1472459628812960772", ReferenceId = "3", ParentReferenceId = "2", Type = "aliquam", Content = "{\"Body\":\"Praesent eget turpis at dolor efficitur dapibus nec at augue.\",\"Name\":\"sit amet\"}", ChildCount = 1 },
                new { Id = "1472459628812960773", ReferenceId = "4", ParentReferenceId = "3", Type = "maecenas", Content = "{\"Body\":\"Duis in lorem aliquet, mattis nisi at, finibus urna.\",\"Name\":\"consectetur\"}", ChildCount = 3 },
                new { Id = "1472459628812960774", ReferenceId = "5", ParentReferenceId = "4", Type = "proin", Content = "{\"Body\":\"Curabitur id diam id elit malesuada sollicitudin quis nec velit.\",\"Name\":\"adipiscing\"}", ChildCount = 1 },
                new { Id = "1472459628812960775", ReferenceId = "9", ParentReferenceId = "4", Type = "proin", Content = "{\"Body\":\"Mauris ultrices ex et velit condimentum vestibulum vel ac diam.\",\"Name\":\"elit.a\"}", ChildCount = 0 },
                new { Id = "1472459628812960776", ReferenceId = "10", ParentReferenceId = "4", Type = "aliquam", Content = "{\"Body\":\"Ut et nisi vestibulum, lobortis orci sed, iaculis odio.\",\"Name\":\"Integer sagittis\"}", ChildCount = 0 },
                new { Id = "1472459628812960777", ReferenceId = "11", ParentReferenceId = "2", Type = "cras", Content = "{\"Body\":\"Sed fermentum augue at erat dignissim tempor.\",\"Name\":\"condimentum\"}", ChildCount = 0 },
                new { Id = "1472459628812960778", ReferenceId = "12", ParentReferenceId = "1", Type = "proin", Content = "{\"Body\":\"Aenean varius neque sit amet diam elementum placerat.\",\"Name\":\"sodales\"}", ChildCount = 1 },
                new { Id = "1472459628812960779", ReferenceId = "13", ParentReferenceId = "12", Type = "aliquam", Content = "{\"Body\":\"Nam vel felis eu mi faucibus consequat vel non justo.\",\"Name\":\"Pellentesque\"}", ChildCount = 6 },
                new { Id = "1472459628812960780", ReferenceId = "14", ParentReferenceId = "13", Type = "proin", Content = "{\"Body\":\"Nam eget massa sed leo suscipit volutpat.\",\"Name\":\"tempor\"}", ChildCount = 0 },
                new { Id = "1472459628812960781", ReferenceId = "15", ParentReferenceId = "13", Type = "aliquam", Content = "{\"Body\":\"Ut consectetur ipsum nec venenatis bibendum.\",\"Name\":\"id\"}", ChildCount = 0 },
                new { Id = "1472459628812960782", ReferenceId = "16", ParentReferenceId = "13", Type = "morbi", Content = "{\"Body\":\"Donec id felis pharetra sapien efficitur vulputate vel et mauris.\",\"Name\":\"velit\"}", ChildCount = 0 },
                new { Id = "1472459628812960783", ReferenceId = "17", ParentReferenceId = "13", Type = "maecenas", Content = "{\"Body\":\"Sed nec erat in nisi porttitor lacinia non et metus.\",\"Name\":\"eget ullamcorper\"}", ChildCount = 0 },
                new { Id = "1472459628821349392", ReferenceId = "18", ParentReferenceId = "13", Type = "morbi", Content = "{\"Body\":\"Etiam semper massa non purus bibendum lacinia.\",\"Name\":\"Vestibulum\"}", ChildCount = 0 },
                new { Id = "1472459628821349393", ReferenceId = "19", ParentReferenceId = "13", Type = "maecenas", Content = "{\"Body\":\"Pellentesque posuere nunc ut iaculis pulvinar.\",\"Name\":\"tempus\"}", ChildCount = 0 },
                new { Id = "1472459628821349394", ReferenceId = "20", ParentReferenceId = "1", Type = "morbi", Content = "{\"Body\":\"Nulla vel orci quis lorem rutrum egestas.\",\"Name\":\"dignissim\"}", ChildCount = 1 },
                new { Id = "1472459628821349395", ReferenceId = "21", ParentReferenceId = "20", Type = "proin", Content = "{\"Body\":\"Donec cursus eros a vestibulum fringilla.\",\"Name\":\"lacus\"}", ChildCount = 1 },
                new { Id = "1472459628821349396", ReferenceId = "22", ParentReferenceId = "21", Type = "cras", Content = "{\"Body\":\"Aliquam gravida purus suscipit, tristique odio sit amet, mattis diam.\",\"Name\":\"eget\"}", ChildCount = 6 },
                new { Id = "1472459628821349397", ReferenceId = "23", ParentReferenceId = "22", Type = "maecenas", Content = "{\"Body\":\"Pellentesque quis orci ornare, pellentesque sapien eu, varius nibh.\",\"Name\":\"dictum\"}", ChildCount = 0 },
                new { Id = "1472459628821349398", ReferenceId = "24", ParentReferenceId = "22", Type = "cras", Content = "{\"Body\":\"Vestibulum laoreet elit ac magna finibus dictum.\",\"Name\":\"metus\"}", ChildCount = 0 },
                new { Id = "1472459628821349399", ReferenceId = "25", ParentReferenceId = "22", Type = "aliquam", Content = "{\"Body\":\"Ut at erat sed erat venenatis consequat quis eget nulla.\",\"Name\":\"finibus\"}", ChildCount = 0 },
                new { Id = "1472459628821349400", ReferenceId = "26", ParentReferenceId = "22", Type = "cras", Content = "{\"Body\":\"Vestibulum ut dolor tincidunt, laoreet nunc bibendum, volutpat ex.\",\"Name\":\"euismod\"}", ChildCount = 0 },
                new { Id = "1472459628821349401", ReferenceId = "27", ParentReferenceId = "22", Type = "morbi", Content = "{\"Body\":\"Morbi quis neque ac mauris sollicitudin tincidunt et ac mauris.\",\"Name\":\"Maecenas\"}", ChildCount = 0 },
                new { Id = "1472459628821349402", ReferenceId = "28", ParentReferenceId = "22", Type = "maecenas", Content = "{\"Body\":\"Nunc bibendum ex sed massa laoreet, non maximus ex venenatis.\",\"Name\":\"convallis\"}", ChildCount = 0 },
                new { Id = "1472459628821349403", ReferenceId = "30", ParentReferenceId = "29", Type = "morbi", Content = "{\"Body\":\"Morbi blandit odio ut tellus laoreet, vel imperdiet tortor tristique.\",\"Name\":\"tortor\"}", ChildCount = 0 },
                new { Id = "1472459628821349404", ReferenceId = "31", ParentReferenceId = "29", Type = "morbi", Content = "{\"Body\":\"Integer vestibulum mi vel auctor ullamcorper.\",\"Name\":\"at\"}", ChildCount = 0 },
                new { Id = "1472459628821349405", ReferenceId = "32", ParentReferenceId = "29", Type = "cras", Content = "{\"Body\":\"Vivamus sed urna sit amet elit facilisis feugiat.\",\"Name\":\"dapibus\"}", ChildCount = 0 },
                new { Id = "1472459628821349406", ReferenceId = "33", ParentReferenceId = "29", Type = "cras", Content = "{\"Body\":\"Aliquam sed felis efficitur, auctor erat sed, porta arcu.\",\"Name\":\"viverra\"}", ChildCount = 0 },
                new { Id = "1472459628829738015", ReferenceId = "34", ParentReferenceId = "29", Type = "maecenas", Content = "{\"Body\":\"Duis a lorem nec eros euismod eleifend.\",\"Name\":\"Mauris\"}", ChildCount = 0 },
                new { Id = "1472459628829738016", ReferenceId = "35", ParentReferenceId = "29", Type = "maecenas", Content = "{\"Body\":\"Curabitur iaculis velit gravida, tempor tellus ullamcorper, ultrices felis.\",\"Name\":\"gravida sem\"}", ChildCount = 0 },
                new { Id = "1472459628829738017", ReferenceId = "36", ParentReferenceId = "29", Type = "aliquam", Content = "{\"Body\":\"Duis non nisi in turpis interdum accumsan.\",\"Name\":\"vel\"}", ChildCount = 0 },
                new { Id = "1472459628829738018", ReferenceId = "37", ParentReferenceId = "29", Type = "aliquam", Content = "{\"Body\":\"Sed ut purus sit amet mi lacinia bibendum sit amet sit amet augue.\",\"Name\":\"tellus\"}", ChildCount = 0 },
                new { Id = "1472459628829738019", ReferenceId = "38", ParentReferenceId = "29", Type = "proin", Content = "{\"Body\":\"Curabitur a sapien vitae dui rutrum accumsan.\",\"Name\":\"pharetra\"}", ChildCount = 0 },
                new { Id = "1472459628829738020", ReferenceId = "39", ParentReferenceId = "29", Type = "aliquam", Content = "{\"Body\":\"Integer tempus ipsum mattis auctor hendrerit.\",\"Name\":\"suscipi\"}", ChildCount = 0 },
                new { Id = "1472459628829738021", ReferenceId = "40", ParentReferenceId = "29", Type = "proin", Content = "{\"Body\":\"Nunc sollicitudin dolor sed nisi luctus, sed tincidunt est scelerisque.\",\"Name\":\"Suspendisse\"}", ChildCount = 0 },
                new { Id = "1472459628829738022", ReferenceId = "41", ParentReferenceId = "29", Type = "morbi", Content = "{\"Body\":\"Proin ultrices ligula eget lacus tempor tincidunt.\",\"Name\":\"bibendum\"}", ChildCount = 0 },
                new { Id = "1472459628829738023", ReferenceId = "6", ParentReferenceId = "5", Type = "proin", Content = "{\"Body\":\"Proin ut nisl a diam pharetra auctor.\",\"Name\":\"consectetur\"}", ChildCount = 1 },
                new { Id = "1472459628829738024", ReferenceId = "7", ParentReferenceId = "6", Type = "maecenas", Content = "{\"Body\":\"Vestibulum id lacus ac mi pharetra congue.\",\"Name\":\"malesuada\"}", ChildCount = 1 },
                new { Id = "1472459628829738025", ReferenceId = "8", ParentReferenceId = "7", Type = "cras", Content = "{\"Body\":\"Ut facilisis dui a aliquet auctor.\",\"Name\":\"aliquet\"}", ChildCount = 0 },
                new { Id = "1472459628821349499", ReferenceId = "29", ParentReferenceId = "1", Type = "aliquam", Content = "{\"Body\":\"Praesent sit amet mattis justo, in molestie arcu.\",\"Name\":\"massa accumsan\"}", ChildCount = 12 }
            };

            var assertCount = 0;

            ConnectAndPrepareDatabase(command =>
            {
                command
                    .CommandText = $"{command.CommandText};select get_hierarchy('website');";

                var hierarchy = JObject.Parse((string)command.ExecuteScalar());

                Assert.Equal("1472538848721372202", (string)hierarchy["Id"]);
                Assert.Equal("website", (string)hierarchy["Key"]);
                Assert.IsType<DateTime>((DateTime)hierarchy["CreatedAt"]);
                Assert.IsType<DateTime>((DateTime)hierarchy["UpdatedAt"]);

                Traverse((JObject)hierarchy["Root"], node =>
                {
                    var assertNodeData = assertsNodeData
                        .Where(nodeData => nodeData.Id == (string)node["Id"])
                        .Single();

                    Assert.Equal(assertNodeData.Id, (string)node["Id"]);
                    Assert.Equal(assertNodeData.ReferenceId, (string)node["$id"]);
                    Assert.Equal(assertNodeData.ParentReferenceId, (string)(node["Parent"].HasValues ? node["Parent"]["$ref"] : null));
                    Assert.Equal(assertNodeData.Type, (string)node["Type"]);
                    Assert.IsType<DateTime>((DateTime)node["CreatedAt"]);
                    Assert.IsType<DateTime>((DateTime)node["UpdatedAt"]);
                    Assert.Equal(assertNodeData.Content, (string)node["Content"]);
                    Assert.Equal(assertNodeData.ChildCount, ((JArray)node["Children"]).Count);

                    assertCount++;
                });

                Assert.Equal(assertCount, assertsNodeData.Length);
            },
             @".\TestData\HierarchyTests.sql");
        }

        [Fact]
        public void GivenNoDataIntheDatabaseWhenAttemptingToLocatesNodesThenAEmptyArrayIsReturned()
        {
            ConnectAndPrepareDatabase(command =>
            {
                command
                    .CommandText = $"{command.CommandText};select get_hierarchy_ids('website');";

                var actual = (string[])command.ExecuteScalar();

                Assert.Equal(0, actual.Length);
            });
        }

        [Fact]
        public void GivenNoDataInTheDatabaseWhenAttemptingToABuildAHierarchyThenReturnAnEmptyObject()
        {
            ConnectAndPrepareDatabase(command =>
            {
                command
                    .CommandText = $"{command.CommandText};select get_hierarchy('website');";

                Assert.NotNull(JObject.Parse((string)command.ExecuteScalar()));
            });
        }

        [Fact]
        public void GivenATreeStructuredJsonDocumentsWhenDeserialisedThenEachNodeShouldHaveThierParentReferencesCorrectlySet()
        {
            ConnectAndPrepareDatabase(command =>
            {
                const string Root = "1472459628771017730";

                command
                    .CommandText = $"{command.CommandText};select get_hierarchy('website');";

                var hierarchy = JsonConvert.DeserializeObject<Hierarchy>((string)command.ExecuteScalar());

                Traverse(hierarchy.Root, (node) =>
                {
                    if (node.Id == Root)
                    {
                        Assert.True(node.Parent == null);
                    }
                    else
                    {
                        Assert.True(node.Parent != null);
                    }
                });
            },
            @".\TestData\HierarchyTests.sql");
        }

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

        private void ConnectAndPrepareDatabase(Action<NpgsqlCommand> test, params string[] scripts)
        {
            using (var connection = new NpgsqlConnection("Host=localhost;Username=user_dukaan_web;Password=password123;Database=dukaan;Search Path=dukaan.web"))
            {
                var sqlBuilder = new StringBuilder(File.ReadAllText(AppendCurrentDirectoryPath(@".\Schema.sql")));

                foreach (var script in scripts)
                {
                    sqlBuilder.AppendLine(File.ReadAllText(AppendCurrentDirectoryPath(script)));
                }

                using (var command = new NpgsqlCommand(sqlBuilder.ToString(), connection))
                {
                    connection.Open();
                    test(command);
                }
            }
        }

        private void Traverse(Node node, Action<Node> callback)
        {
            for (int index = 0; index < node.Children.Count; index++)
            {
                Traverse(node.Children[index], callback);
            }

            callback(node);
        }

        private void TraverseWithParent(Node node, Node parent, Action<Node, Node> callback)
        {
            for (int index = 0; index < node.Children.Count; index++)
            {
                TraverseWithParent(node.Children[index], node, callback);
            }

            callback(node, parent);
        }

        private string AppendCurrentDirectoryPath(string path)
        {
            return Path.Combine(AppContext.BaseDirectory, path);
        }

        #region build hierarchy model

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

            TraverseWithParent(root, null, (node, parent) =>
            {
                node.Parent = parent;
            });

            return root;
        }

        #endregion
    }
}
