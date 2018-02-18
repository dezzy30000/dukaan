using dukaan.web.Infrastructure.Json;
using dukaan.web.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace dukaan.dataapi.tests
{
    public class HierarchyTests
    {
        private readonly IConfiguration _configuration;
        private readonly string _liveSchema = "dukaan.web";
        private readonly string _testSchema = typeof(HierarchyTests).GetTypeInfo().Namespace;

        public HierarchyTests()
        {
            _configuration = new ConfigurationBuilder()
                .AddUserSecrets<HierarchyTests>()
                .Build();
        }

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

            TearDownDatabase();
        }

        [Fact]
        public void GivenASkeletonHierarchyWhenHydratedThenTheHierarchyIsPopulatedWithNodeData()
        {
            var assertsNodeData = new[]
            {
                new { Id = "1472459628771017730", ReferenceId = "1", ParentReferenceId = (string)null, Type = "cras", ContentBody = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.", ContentName = "lorem", ChildCount = 4 },
                new { Id = "1472459628812960771", ReferenceId = "2", ParentReferenceId = "1", Type = "morbi", ContentBody = "Suspendisse a arcu a tortor scelerisque auctor vitae et nisl.", ContentName = "ipsum dolor", ChildCount = 2 },
                new { Id = "1472459628812960772", ReferenceId = "3", ParentReferenceId = "2", Type = "aliquam", ContentBody = "Praesent eget turpis at dolor efficitur dapibus nec at augue.", ContentName = "sit amet", ChildCount = 1 },
                new { Id = "1472459628812960773", ReferenceId = "4", ParentReferenceId = "3", Type = "maecenas", ContentBody = "Duis in lorem aliquet, mattis nisi at, finibus urna.", ContentName = "consectetur", ChildCount = 3 },
                new { Id = "1472459628812960774", ReferenceId = "5", ParentReferenceId = "4", Type = "proin", ContentBody = "Curabitur id diam id elit malesuada sollicitudin quis nec velit.", ContentName = "adipiscing", ChildCount = 1 },
                new { Id = "1472459628812960775", ReferenceId = "9", ParentReferenceId = "4", Type = "proin", ContentBody = "Mauris ultrices ex et velit condimentum vestibulum vel ac diam.", ContentName = "elit.a", ChildCount = 0 },
                new { Id = "1472459628812960776", ReferenceId = "10", ParentReferenceId = "4", Type = "aliquam", ContentBody = "Ut et nisi vestibulum, lobortis orci sed, iaculis odio.", ContentName = "Integer sagittis", ChildCount = 0 },
                new { Id = "1472459628812960777", ReferenceId = "11", ParentReferenceId = "2", Type = "cras", ContentBody = "Sed fermentum augue at erat dignissim tempor.", ContentName = "condimentum", ChildCount = 0 },
                new { Id = "1472459628812960778", ReferenceId = "12", ParentReferenceId = "1", Type = "proin", ContentBody = "Aenean varius neque sit amet diam elementum placerat.", ContentName = "sodales", ChildCount = 1 },
                new { Id = "1472459628812960779", ReferenceId = "13", ParentReferenceId = "12", Type = "aliquam", ContentBody = "Nam vel felis eu mi faucibus consequat vel non justo.", ContentName = "Pellentesque", ChildCount = 6 },
                new { Id = "1472459628812960780", ReferenceId = "14", ParentReferenceId = "13", Type = "proin", ContentBody = "Nam eget massa sed leo suscipit volutpat.", ContentName = "tempor", ChildCount = 0 },
                new { Id = "1472459628812960781", ReferenceId = "15", ParentReferenceId = "13", Type = "aliquam", ContentBody = "Ut consectetur ipsum nec venenatis bibendum.", ContentName = "id", ChildCount = 0 },
                new { Id = "1472459628812960782", ReferenceId = "16", ParentReferenceId = "13", Type = "morbi", ContentBody = "Donec id felis pharetra sapien efficitur vulputate vel et mauris.", ContentName = "velit", ChildCount = 0 },
                new { Id = "1472459628812960783", ReferenceId = "17", ParentReferenceId = "13", Type = "maecenas", ContentBody = "Sed nec erat in nisi porttitor lacinia non et metus.", ContentName = "eget ullamcorper", ChildCount = 0 },
                new { Id = "1472459628821349392", ReferenceId = "18", ParentReferenceId = "13", Type = "morbi", ContentBody = "Etiam semper massa non purus bibendum lacinia.", ContentName = "Vestibulum", ChildCount = 0 },
                new { Id = "1472459628821349393", ReferenceId = "19", ParentReferenceId = "13", Type = "maecenas", ContentBody = "Pellentesque posuere nunc ut iaculis pulvinar.", ContentName = "tempus", ChildCount = 0 },
                new { Id = "1472459628821349394", ReferenceId = "20", ParentReferenceId = "1", Type = "morbi", ContentBody = "Nulla vel orci quis lorem rutrum egestas.", ContentName = "dignissim", ChildCount = 1 },
                new { Id = "1472459628821349395", ReferenceId = "21", ParentReferenceId = "20", Type = "proin", ContentBody = "Donec cursus eros a vestibulum fringilla.", ContentName = "lacus", ChildCount = 1 },
                new { Id = "1472459628821349396", ReferenceId = "22", ParentReferenceId = "21", Type = "cras", ContentBody = "Aliquam gravida purus suscipit, tristique odio sit amet, mattis diam.", ContentName = "eget", ChildCount = 6 },
                new { Id = "1472459628821349397", ReferenceId = "23", ParentReferenceId = "22", Type = "maecenas", ContentBody = "Pellentesque quis orci ornare, pellentesque sapien eu, varius nibh.", ContentName = "dictum", ChildCount = 0 },
                new { Id = "1472459628821349398", ReferenceId = "24", ParentReferenceId = "22", Type = "cras", ContentBody = "Vestibulum laoreet elit ac magna finibus dictum.", ContentName = "metus", ChildCount = 0 },
                new { Id = "1472459628821349399", ReferenceId = "25", ParentReferenceId = "22", Type = "aliquam", ContentBody = "Ut at erat sed erat venenatis consequat quis eget nulla.", ContentName = "finibus", ChildCount = 0 },
                new { Id = "1472459628821349400", ReferenceId = "26", ParentReferenceId = "22", Type = "cras", ContentBody = "Vestibulum ut dolor tincidunt, laoreet nunc bibendum, volutpat ex.", ContentName = "euismod", ChildCount = 0 },
                new { Id = "1472459628821349401", ReferenceId = "27", ParentReferenceId = "22", Type = "morbi", ContentBody = "Morbi quis neque ac mauris sollicitudin tincidunt et ac mauris.", ContentName = "Maecenas", ChildCount = 0 },
                new { Id = "1472459628821349402", ReferenceId = "28", ParentReferenceId = "22", Type = "maecenas", ContentBody = "Nunc bibendum ex sed massa laoreet, non maximus ex venenatis.", ContentName = "convallis", ChildCount = 0 },
                new { Id = "1472459628821349403", ReferenceId = "30", ParentReferenceId = "29", Type = "morbi", ContentBody = "Morbi blandit odio ut tellus laoreet, vel imperdiet tortor tristique.", ContentName = "tortor", ChildCount = 0 },
                new { Id = "1472459628821349404", ReferenceId = "31", ParentReferenceId = "29", Type = "morbi", ContentBody = "Integer vestibulum mi vel auctor ullamcorper.", ContentName = "at", ChildCount = 0 },
                new { Id = "1472459628821349405", ReferenceId = "32", ParentReferenceId = "29", Type = "cras", ContentBody = "Vivamus sed urna sit amet elit facilisis feugiat.", ContentName = "dapibus", ChildCount = 0 },
                new { Id = "1472459628821349406", ReferenceId = "33", ParentReferenceId = "29", Type = "cras", ContentBody = "Aliquam sed felis efficitur, auctor erat sed, porta arcu.", ContentName = "viverra", ChildCount = 0 },
                new { Id = "1472459628829738015", ReferenceId = "34", ParentReferenceId = "29", Type = "maecenas", ContentBody = "Duis a lorem nec eros euismod eleifend.", ContentName = "Mauris", ChildCount = 0 },
                new { Id = "1472459628829738016", ReferenceId = "35", ParentReferenceId = "29", Type = "maecenas", ContentBody = "Curabitur iaculis velit gravida, tempor tellus ullamcorper, ultrices felis.", ContentName = "gravida sem", ChildCount = 0 },
                new { Id = "1472459628829738017", ReferenceId = "36", ParentReferenceId = "29", Type = "aliquam", ContentBody = "Duis non nisi in turpis interdum accumsan.", ContentName = "vel", ChildCount = 0 },
                new { Id = "1472459628829738018", ReferenceId = "37", ParentReferenceId = "29", Type = "aliquam", ContentBody = "Sed ut purus sit amet mi lacinia bibendum sit amet sit amet augue.", ContentName = "tellus", ChildCount = 0 },
                new { Id = "1472459628829738019", ReferenceId = "38", ParentReferenceId = "29", Type = "proin", ContentBody = "Curabitur a sapien vitae dui rutrum accumsan.", ContentName = "pharetra", ChildCount = 0 },
                new { Id = "1472459628829738020", ReferenceId = "39", ParentReferenceId = "29", Type = "aliquam", ContentBody = "Integer tempus ipsum mattis auctor hendrerit.", ContentName = "suscipi", ChildCount = 0 },
                new { Id = "1472459628829738021", ReferenceId = "40", ParentReferenceId = "29", Type = "proin", ContentBody = "Nunc sollicitudin dolor sed nisi luctus, sed tincidunt est scelerisque.", ContentName = "Suspendisse", ChildCount = 0 },
                new { Id = "1472459628829738022", ReferenceId = "41", ParentReferenceId = "29", Type = "morbi", ContentBody = "Proin ultrices ligula eget lacus tempor tincidunt.", ContentName = "bibendum", ChildCount = 0 },
                new { Id = "1472459628829738023", ReferenceId = "6", ParentReferenceId = "5", Type = "proin", ContentBody = "Proin ut nisl a diam pharetra auctor.", ContentName = "consectetur", ChildCount = 1 },
                new { Id = "1472459628829738024", ReferenceId = "7", ParentReferenceId = "6", Type = "maecenas", ContentBody = "Vestibulum id lacus ac mi pharetra congue.", ContentName = "malesuada", ChildCount = 1 },
                new { Id = "1472459628829738025", ReferenceId = "8", ParentReferenceId = "7", Type = "cras", ContentBody = "Ut facilisis dui a aliquet auctor.", ContentName = "aliquet", ChildCount = 0 },
                new { Id = "1472459628821349499", ReferenceId = "29", ParentReferenceId = "1", Type = "aliquam", ContentBody = "Praesent sit amet mattis justo, in molestie arcu.", ContentName = "massa accumsan", ChildCount = 12 }
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
                    Assert.Equal(assertNodeData.ContentBody, (string)node["Content"]["Body"]);
                    Assert.Equal(assertNodeData.ContentName, (string)node["Content"]["Name"]);
                    Assert.Equal(assertNodeData.ChildCount, ((JArray)node["Children"]).Count);

                    assertCount++;
                });

                Assert.Equal(assertCount, assertsNodeData.Length);
            },
             @".\TestData\HierarchyTests.sql");

            TearDownDatabase();
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

            TearDownDatabase();
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

            TearDownDatabase();
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

            TearDownDatabase();
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

            TearDownDatabase();
        }

        #region Helper methods

        private void ConnectAndPrepareDatabase(Action<NpgsqlCommand> test, params string[] scripts)
        {
            var sqlBuilder = new StringBuilder(File.ReadAllText(AppendCurrentDirectoryPath(@".\Schema.sql")));

            foreach (var script in scripts)
            {
                sqlBuilder.AppendLine(File.ReadAllText(AppendCurrentDirectoryPath(script)));
            }

            foreach (var map in new Func<string, string>[]
            {
                schemaName => $"drop schema if exists \"{schemaName}\" cascade;",
                schemaName => $"create schema \"{schemaName}\";",
                schemaName => $"set search_path=\"{schemaName}\";"
            })
            {
                sqlBuilder.Replace(map(_liveSchema), map(_testSchema));
            }

            var sql = sqlBuilder.ToString();

            Assert.DoesNotContain(_liveSchema, sql);

            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("dukaandb")))
            {
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    connection.Open();
                    test(command);
                }
            }
        }

        private void TearDownDatabase()
        {
            var sql = $"drop schema if exists \"{_testSchema}\" cascade;";

            Assert.DoesNotContain(_liveSchema, sql);

            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("dukaandb")))
            {
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        private void Traverse(JObject node, Action<JObject> asserts)
        {
            foreach (var child in ((JArray)node["Children"]))
            {
                Traverse((JObject)child, asserts);
            }

            asserts(node);
        }

        private void Traverse(Node node, Action<Node> callback)
        {
            for (int index = 0; index < node.Children.Length; index++)
            {
                Traverse(node.Children[index], callback);
            }

            callback(node);
        }

        private void TraverseWithParent(Node node, Node parent, Action<Node, Node> callback)
        {
            for (int index = 0; index < node.Children.Length; index++)
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
                Children = new[]
                {
                    new Node
                    {
                        Id = "1472459628812960771",
                        Children = new []
                        {
                            new Node
                            {
                                Id = "1472459628812960772",
                                Children = new []
                                {
                                    new Node
                                    {
                                        Id = "1472459628812960773",
                                        Children = new []
                                        {
                                            new Node
                                            {
                                                Id = "1472459628812960774",
                                                Children = new []
                                                {
                                                    new Node
                                                    {
                                                        Id = "1472459628829738023",
                                                        Children = new []
                                                        {
                                                            new Node
                                                            {
                                                                Id = "1472459628829738024",
                                                                Children = new []
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
                        Children = new []
                        {
                            new Node
                            {
                                Id = "1472459628812960779",
                                Children = new []
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
                        Children = new []
                        {
                            new Node
                            {
                                Id = "1472459628821349395",
                                Children = new []
                                {
                                    new Node
                                    {
                                        Id = "1472459628821349396",
                                        Children = new []
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
                        Children = new []
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

        #endregion
    }
}
