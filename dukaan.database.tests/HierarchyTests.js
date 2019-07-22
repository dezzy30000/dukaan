var nodeId = /^\d{19}$/i;
var iso8601full = /^\d{4}-\d\d-\d\dT\d\d:\d\d:\d\d(\.\d+)?(([+-]\d\d:\d\d)|Z)?$/i;

var tests = [{
        'Given a hierarchy locate all node ids when parsed then a list of ids is returned': function(){
            var expected = [
                '1485616302037926914', '1485616302037926915', '1485616302037926916', '1485616302037926917', '1485616302037926918',
                '1485616302037926919', '1485616302037926920', '1485616302037926921', '1485616302037926922', '1485616302037926923',
                '1485616302037926924', '1485616302037926925', '1485616302037926926', '1485616302037926927', '1485616302037926928',
                '1485616302037926929', '1485616302037926930', '1485616302037926931', '1485616302004372481'];

            var result = plv8.execute('SET search_path="dukaan.web";SELECT get_hierarchy_ids(\'website\') AS ids;');

            assert.equal(result.length, 1, 'should only return 1 row.');
            assert.equal(result[0].ids.length, expected.length, 'should contain ' + expected.length + ' hierarchy ids.');

            for(var index = 0; index < expected.length; index++){
                assert.equal(result[0].ids[index], expected[index], 'id ' + result[0].ids[index] + ' (actual) should match ' + expected[index] + ' (expected).');
            }
        }
    },{
        'Given a skeleton hierarchy when hydrated then the hierarchy is populated with node data': function(){

            var expectations = [
                {'$id' : '2', '$ref': '1', 'childrenCount': 0, 'id': '1485616302037926914', 'title': 'Wish List', 'type': 'WishList'},
                {'$id' : '3', '$ref': '1', 'childrenCount': 0, 'id': '1485616302037926915', 'title': 'Typography', 'type': 'Typography'},
                {'$id' : '4', '$ref': '1', 'childrenCount': 0, 'id': '1485616302037926916', 'title': 'Register', 'type': 'Register'},
                {'$id' : '5', '$ref': '1', 'childrenCount': 0, 'id': '1485616302037926917', 'title': 'Products', 'type': 'Products'},
                {'$id' : '6', '$ref': '1', 'childrenCount': 0, 'id': '1485616302037926918', 'title': 'Login', 'type': 'Login'},
                {'$id' : '7', '$ref': '1', 'childrenCount': 0, 'id': '1485616302037926919', 'title': 'Details', 'type': 'Details'},
                {'$id' : '8', '$ref': '1', 'childrenCount': 0, 'id': '1485616302037926920', 'title': 'Contact', 'type': 'Contact'},
                {'$id' : '9', '$ref': '1', 'childrenCount': 0, 'id': '1485616302037926921', 'title': 'Compare', 'type': 'Compare'},
                {'$id' : '10', '$ref': '1', 'childrenCount': 0, 'id': '1485616302037926922', 'title': 'Checkout Wizard', 'type': 'CheckoutWizard'},
                {'$id' : '11', '$ref': '1', 'childrenCount': 0, 'id': '1485616302037926923', 'title': 'Checkout', 'type': 'Checkout'},
                {'$id' : '12', '$ref': '1', 'childrenCount': 0, 'id': '1485616302037926924', 'title': 'Blog', 'type': 'Blog'},
                {'$id' : '13', '$ref': '1', 'childrenCount': 0, 'id': '1485616302037926925', 'title': 'Blog Details', 'type': 'BlogDetails'},
                {'$id' : '14', '$ref': '1', 'childrenCount': 0, 'id': '1485616302037926926', 'title': 'Account Profile', 'type': 'AccountProfile'},
                {'$id' : '15', '$ref': '1', 'childrenCount': 0, 'id': '1485616302037926927', 'title': 'Account Password', 'type': 'AccountPassword'},
                {'$id' : '16', '$ref': '1', 'childrenCount': 0, 'id': '1485616302037926928', 'title': 'Account History', 'type': 'AccountHistory'},
                {'$id' : '17', '$ref': '1', 'childrenCount': 0, 'id': '1485616302037926929', 'title': 'Account Address', 'type': 'AccountAddress'},
                {'$id' : '18', '$ref': '1', 'childrenCount': 0, 'id': '1485616302037926930', 'title': 'About', 'type': 'About'},
                {'$id' : '19', '$ref': '1', 'childrenCount': 0, 'id': '1485616302037926931', 'title': 'Cart', 'type': 'Cart'}
            ];

            var result = plv8.execute('SET search_path="dukaan.web";SELECT get_hierarchy(\'website\')::json AS hierarchy;');

            assert.equal(result.length, 1, 'should only return 1 row.');

            var hierarchyNode = result[0].hierarchy;

            assert.equal(nodeId.test(hierarchyNode.Id), true, 'should return a node id. id returned is ' + hierarchyNode.Id + '.');
            assert.equal(hierarchyNode.Key, 'website', 'should return a key. key returned is ' + hierarchyNode.Key + '.');
            assert.equal(hierarchyNode.Root !== null, true, 'node should return a root node.');
            assert.equal(typeof(hierarchyNode.Root), 'object', 'node should return a root object node.');
            assert.equal(iso8601full.test(hierarchyNode.CreatedAt), true, 'node should return a valid created at date.');
            assert.equal(iso8601full.test(hierarchyNode.UpdatedAt), true, 'node should return a valid updated at date.');

            var rootNode = hierarchyNode.Root;

            assert.equal(rootNode.$id, '1', 'should return a nodeid. id returned is ' + rootNode.$id + '.');
            assert.equal(Object.keys(rootNode)[0], '$id', 'metadata items should be before other properties of an object.')
            assert.equal(rootNode.Parent, null, 'node should not have a parent.');
            assert.equal(rootNode.Children.length, 18, 'the node should have an array of children. the length returned is ' + rootNode.Children.length + '.');
            assert.equal(rootNode.Id, "1485616302004372481", 'should return a node id. id returned is ' + rootNode.Id + '.');
            assert.equal(typeof(rootNode.Content), 'object', 'should have a content node.');
            assert.equal(rootNode.Content.Title, 'Home', 'content in the node should have a title.');
            assert.equal(rootNode.Type, 'Home', 'node should have a type.');
            assert.equal(iso8601full.test(rootNode.CreatedAt), true, 'node should return a valid created at date.');
            assert.equal(iso8601full.test(rootNode.UpdatedAt), true, 'node should return a valid updated at date.');

            assert.equal(rootNode.Children.length === expectations.length, true, 'number of expecations should match up with number of root children.');

            for(var index = 0; index < rootNode.Children.length; index++)
            {
                var node = rootNode.Children[index];
                var expectation = expectations[index];

                assert.equal(node.$id, expectation.$id, 'should return a nodeid. id returned is ' + node.$id + '.');
                assert.equal(Object.keys(node)[0], '$id', 'metadata items should be before other properties of an object.')
                assert.equal(typeof(node.Parent), 'object', 'node should have a parent.');
                assert.equal(node.Parent.$ref, expectation.$ref, 'node should point to a parent.');
                assert.equal(node.Children.length, expectation.childrenCount, 'the node should have an array of children. the length returned is ' + node.Children.length + '.');
                assert.equal(node.Id, expectation.id, 'should return a node id. id returned is ' + node.Id + '.');
                assert.equal(typeof(node.Content), 'object', 'should have a content node.');
                assert.equal(node.Content.Title, expectation.title, 'content in the node should have a title.');
                assert.equal(node.Type, expectation.type, 'node should have a type.');
                assert.equal(iso8601full.test(node.CreatedAt), true, 'node should return a valid created at date.');
                assert.equal(iso8601full.test(node.UpdatedAt), true, 'node should return a valid updated at date.');
            }
        }
    }
];