using Newtonsoft.Json.Linq;
using Npgsql;
using System;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

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
            @".\Scripts\Data\HierarchyTests.sql");
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
                new { Id = "1472459628771017730", ParentId = "", Type = "cras", ContentBody = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.", ContentName = "lorem", ChildCount = 4 },
                new { Id = "1472459628812960771", ParentId = "1472459628771017730", Type = "morbi", ContentBody = "Suspendisse a arcu a tortor scelerisque auctor vitae et nisl.", ContentName = "ipsum dolor", ChildCount = 2 },
                new { Id = "1472459628812960772", ParentId = "1472459628812960771", Type = "aliquam", ContentBody = "Praesent eget turpis at dolor efficitur dapibus nec at augue.", ContentName = "sit amet", ChildCount = 1 },
                new { Id = "1472459628812960773", ParentId = "1472459628812960772", Type = "maecenas", ContentBody = "Duis in lorem aliquet, mattis nisi at, finibus urna.", ContentName = "consectetur", ChildCount = 3 },
                new { Id = "1472459628812960774", ParentId = "1472459628812960773", Type = "proin", ContentBody = "Curabitur id diam id elit malesuada sollicitudin quis nec velit.", ContentName = "adipiscing", ChildCount = 1 },
                new { Id = "1472459628812960775", ParentId = "1472459628812960773", Type = "proin", ContentBody = "Mauris ultrices ex et velit condimentum vestibulum vel ac diam.", ContentName = "elit.a", ChildCount = 0 },
                new { Id = "1472459628812960776", ParentId = "1472459628812960773", Type = "aliquam", ContentBody = "Ut et nisi vestibulum, lobortis orci sed, iaculis odio.", ContentName = "Integer sagittis", ChildCount = 0 },
                new { Id = "1472459628812960777", ParentId = "1472459628812960771", Type = "cras", ContentBody = "Sed fermentum augue at erat dignissim tempor.", ContentName = "condimentum", ChildCount = 0 },
                new { Id = "1472459628812960778", ParentId = "1472459628771017730", Type = "proin", ContentBody = "Aenean varius neque sit amet diam elementum placerat.", ContentName = "sodales", ChildCount = 1 },
                new { Id = "1472459628812960779", ParentId = "1472459628812960778", Type = "aliquam", ContentBody = "Nam vel felis eu mi faucibus consequat vel non justo.", ContentName = "Pellentesque", ChildCount = 6 },
                new { Id = "1472459628812960780", ParentId = "1472459628812960779", Type = "proin", ContentBody = "Nam eget massa sed leo suscipit volutpat.", ContentName = "tempor", ChildCount = 0 },
                new { Id = "1472459628812960781", ParentId = "1472459628812960779", Type = "aliquam", ContentBody = "Ut consectetur ipsum nec venenatis bibendum.", ContentName = "id", ChildCount = 0 },
                new { Id = "1472459628812960782", ParentId = "1472459628812960779", Type = "morbi", ContentBody = "Donec id felis pharetra sapien efficitur vulputate vel et mauris.", ContentName = "velit", ChildCount = 0 },
                new { Id = "1472459628812960783", ParentId = "1472459628812960779", Type = "maecenas", ContentBody = "Sed nec erat in nisi porttitor lacinia non et metus.", ContentName = "eget ullamcorper", ChildCount = 0 },
                new { Id = "1472459628821349392", ParentId = "1472459628812960779", Type = "morbi", ContentBody = "Etiam semper massa non purus bibendum lacinia.", ContentName = "Vestibulum", ChildCount = 0 },
                new { Id = "1472459628821349393", ParentId = "1472459628812960779", Type = "maecenas", ContentBody = "Pellentesque posuere nunc ut iaculis pulvinar.", ContentName = "tempus", ChildCount = 0 },
                new { Id = "1472459628821349394", ParentId = "1472459628771017730", Type = "morbi", ContentBody = "Nulla vel orci quis lorem rutrum egestas.", ContentName = "dignissim", ChildCount = 1 },
                new { Id = "1472459628821349395", ParentId = "1472459628821349394", Type = "proin", ContentBody = "Donec cursus eros a vestibulum fringilla.", ContentName = "lacus", ChildCount = 1 },
                new { Id = "1472459628821349396", ParentId = "1472459628821349395", Type = "cras", ContentBody = "Aliquam gravida purus suscipit, tristique odio sit amet, mattis diam.", ContentName = "eget", ChildCount = 6 },
                new { Id = "1472459628821349397", ParentId = "1472459628821349396", Type = "maecenas", ContentBody = "Pellentesque quis orci ornare, pellentesque sapien eu, varius nibh.", ContentName = "dictum", ChildCount = 0 },
                new { Id = "1472459628821349398", ParentId = "1472459628821349396", Type = "cras", ContentBody = "Vestibulum laoreet elit ac magna finibus dictum.", ContentName = "metus", ChildCount = 0 },
                new { Id = "1472459628821349399", ParentId = "1472459628821349396", Type = "aliquam", ContentBody = "Ut at erat sed erat venenatis consequat quis eget nulla.", ContentName = "finibus", ChildCount = 0 },
                new { Id = "1472459628821349400", ParentId = "1472459628821349396", Type = "cras", ContentBody = "Vestibulum ut dolor tincidunt, laoreet nunc bibendum, volutpat ex.", ContentName = "euismod", ChildCount = 0 },
                new { Id = "1472459628821349401", ParentId = "1472459628821349396", Type = "morbi", ContentBody = "Morbi quis neque ac mauris sollicitudin tincidunt et ac mauris.", ContentName = "Maecenas", ChildCount = 0 },
                new { Id = "1472459628821349402", ParentId = "1472459628821349396", Type = "maecenas", ContentBody = "Nunc bibendum ex sed massa laoreet, non maximus ex venenatis.", ContentName = "convallis", ChildCount = 0 },
                new { Id = "1472459628821349403", ParentId = "1472459628821349499", Type = "morbi", ContentBody = "Morbi blandit odio ut tellus laoreet, vel imperdiet tortor tristique.", ContentName = "tortor", ChildCount = 0 },
                new { Id = "1472459628821349404", ParentId = "1472459628821349499", Type = "morbi", ContentBody = "Integer vestibulum mi vel auctor ullamcorper.", ContentName = "at", ChildCount = 0 },
                new { Id = "1472459628821349405", ParentId = "1472459628821349499", Type = "cras", ContentBody = "Vivamus sed urna sit amet elit facilisis feugiat.", ContentName = "dapibus", ChildCount = 0 },
                new { Id = "1472459628821349406", ParentId = "1472459628821349499", Type = "cras", ContentBody = "Aliquam sed felis efficitur, auctor erat sed, porta arcu.", ContentName = "viverra", ChildCount = 0 },
                new { Id = "1472459628829738015", ParentId = "1472459628821349499", Type = "maecenas", ContentBody = "Duis a lorem nec eros euismod eleifend.", ContentName = "Mauris", ChildCount = 0 },
                new { Id = "1472459628829738016", ParentId = "1472459628821349499", Type = "maecenas", ContentBody = "Curabitur iaculis velit gravida, tempor tellus ullamcorper, ultrices felis.", ContentName = "gravida sem", ChildCount = 0 },
                new { Id = "1472459628829738017", ParentId = "1472459628821349499", Type = "aliquam", ContentBody = "Duis non nisi in turpis interdum accumsan.", ContentName = "vel", ChildCount = 0 },
                new { Id = "1472459628829738018", ParentId = "1472459628821349499", Type = "aliquam", ContentBody = "Sed ut purus sit amet mi lacinia bibendum sit amet sit amet augue.", ContentName = "tellus", ChildCount = 0 },
                new { Id = "1472459628829738019", ParentId = "1472459628821349499", Type = "proin", ContentBody = "Curabitur a sapien vitae dui rutrum accumsan.", ContentName = "pharetra", ChildCount = 0 },
                new { Id = "1472459628829738020", ParentId = "1472459628821349499", Type = "aliquam", ContentBody = "Integer tempus ipsum mattis auctor hendrerit.", ContentName = "suscipi", ChildCount = 0 },
                new { Id = "1472459628829738021", ParentId = "1472459628821349499", Type = "proin", ContentBody = "Nunc sollicitudin dolor sed nisi luctus, sed tincidunt est scelerisque.", ContentName = "Suspendisse", ChildCount = 0 },
                new { Id = "1472459628829738022", ParentId = "1472459628821349499", Type = "morbi", ContentBody = "Proin ultrices ligula eget lacus tempor tincidunt.", ContentName = "bibendum", ChildCount = 0 },
                new { Id = "1472459628829738023", ParentId = "1472459628812960774", Type = "proin", ContentBody = "Proin ut nisl a diam pharetra auctor.", ContentName = "consectetur", ChildCount = 1 },
                new { Id = "1472459628829738024", ParentId = "1472459628829738023", Type = "maecenas", ContentBody = "Vestibulum id lacus ac mi pharetra congue.", ContentName = "malesuada", ChildCount = 1 },
                new { Id = "1472459628829738025", ParentId = "1472459628829738024", Type = "cras", ContentBody = "Ut facilisis dui a aliquet auctor.", ContentName = "aliquet", ChildCount = 0 },
                new { Id = "1472459628821349499", ParentId = "1472459628771017730", Type = "aliquam", ContentBody = "Praesent sit amet mattis justo, in molestie arcu.", ContentName = "massa accumsan", ChildCount = 12 }
            };

            ConnectAndPrepareDatabase(command =>
            {
                command
                    .CommandText = $"{command.CommandText};select get_hierarchy('website');";

                var hierarchy = JObject.Parse((string)command.ExecuteScalar());

                Assert.Equal("1472538848721372202", (string)hierarchy["Id"]);
                Assert.Equal("website", (string)hierarchy["Key"]);
                Assert.IsType<DateTime>((DateTime)hierarchy["CreatedAt"]);
                Assert.IsType<DateTime>((DateTime)hierarchy["UpdatedAt"]);

                Traverse((JObject)hierarchy["Body"]["Root"], node =>
                {
                    var assertNodeData = assertsNodeData
                        .Where(nodeData => nodeData.Id == (string)node["Id"])
                        .Single();

                    Assert.Equal(assertNodeData.Id, (string)node["Id"]);
                    Assert.Equal(assertNodeData.ParentId, (string)node["ParentId"]);
                    Assert.Equal(assertNodeData.Type, (string)node["Type"]);
                    Assert.IsType<DateTime>((DateTime)node["CreatedAt"]);
                    Assert.IsType<DateTime>((DateTime)node["UpdatedAt"]);
                    Assert.Equal(assertNodeData.ContentBody, (string)node["Content"]["Body"]);
                    Assert.Equal(assertNodeData.ContentName, (string)node["Content"]["Name"]);
                    Assert.Equal(assertNodeData.ChildCount, ((JArray)node["Children"]).Count);
                });
            },
             @".\Scripts\Data\HierarchyTests.sql");
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

        private void ConnectAndPrepareDatabase(Action<NpgsqlCommand> test, params string[] scripts)
        {
            using (var connection = new NpgsqlConnection("Host=localhost;Username=user_dukaan_web;Password=password123;Database=dukaan"))
            {
                var sqlBuilder = new StringBuilder(File.ReadAllText(@".\Scripts\Schema.sql"));

                foreach (var script in scripts)
                {
                    sqlBuilder.AppendLine(File.ReadAllText(script));
                }

                using (var command = new NpgsqlCommand(sqlBuilder.ToString(), connection))
                {
                    connection.Open();
                    test(command);
                }
            }
        }
    }
}
