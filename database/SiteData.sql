set search_path="dukaan.web";

delete from node_docs;

alter table only node_docs alter column id drop default;

insert into node_docs(id, body, type) values (1485616302004372481, '{"Title":"Home"}', 'Home');
insert into node_docs(id, body, type) values (1485616302037926914, '{"Title":"Wish List"}', 'WishList');
insert into node_docs(id, body, type) values (1485616302037926915, '{"Title":"Typography"}', 'Typography');
insert into node_docs(id, body, type) values (1485616302037926916, '{"Title":"Register"}', 'Register');
insert into node_docs(id, body, type) values (1485616302037926917, '{"Title":"Products"}', 'Products');
insert into node_docs(id, body, type) values (1485616302037926918, '{"Title":"Login"}', 'Login');
insert into node_docs(id, body, type) values (1485616302037926919, '{"Title":"Details"}', 'Details');
insert into node_docs(id, body, type) values (1485616302037926920, '{"Title":"Contact"}', 'Contact');
insert into node_docs(id, body, type) values (1485616302037926921, '{"Title":"Compare"}', 'Compare');
insert into node_docs(id, body, type) values (1485616302037926922, '{"Title":"Checkout Wizard"}', 'CheckoutWizard');
insert into node_docs(id, body, type) values (1485616302037926923, '{"Title":"Checkout"}', 'Checkout');
insert into node_docs(id, body, type) values (1485616302037926924, '{"Title":"Blog Details"}', 'BlogDetails');
insert into node_docs(id, body, type) values (1485616302037926925, '{"Title":"Account Profile"}', 'AccountProfile');
insert into node_docs(id, body, type) values (1485616302037926926, '{"Title":"Account Password"}', 'AccountPassword');
insert into node_docs(id, body, type) values (1485616302037926927, '{"Title":"Account History"}', 'AccountHistory');
insert into node_docs(id, body, type) values (1485616302037926928, '{"Title":"Account Address"}', 'AccountAddress');
insert into node_docs(id, body, type) values (1485616302037926929, '{"Title":"About"}', 'About');

alter table only node_docs alter column id set default id_generator();

delete from hierarchy_docs;

insert into hierarchy_docs(key, body) values ('website', '{"$id":"1","Id":"1485616302004372481","Parent":null,"Children":[{"$id":"2","Id":"1485616302037926914","Parent":{"$ref":"1"},"Children":[]},{"$id":"3","Id":"1485616302037926915","Parent":{"$ref":"1"},"Children":[]},{"$id":"4","Id":"1485616302037926916","Parent":{"$ref":"1"},"Children":[]},{"$id":"5","Id":"1485616302037926917","Parent":{"$ref":"1"},"Children":[]},{"$id":"6","Id":"1485616302037926918","Parent":{"$ref":"1"},"Children":[]},{"$id":"7","Id":"1485616302037926919","Parent":{"$ref":"1"},"Children":[]},{"$id":"8","Id":"1485616302037926920","Parent":{"$ref":"1"},"Children":[]},{"$id":"9","Id":"1485616302037926921","Parent":{"$ref":"1"},"Children":[]},{"$id":"10","Id":"1485616302037926922","Parent":{"$ref":"1"},"Children":[]},{"$id":"11","Id":"1485616302037926923","Parent":{"$ref":"1"},"Children":[]},{"$id":"12","Id":"1485616302037926924","Parent":{"$ref":"1"},"Children":[]},{"$id":"13","Id":"1485616302037926925","Parent":{"$ref":"1"},"Children":[]},{"$id":"14","Id":"1485616302037926926","Parent":{"$ref":"1"},"Children":[]},{"$id":"15","Id":"1485616302037926927","Parent":{"$ref":"1"},"Children":[]},{"$id":"16","Id":"1485616302037926928","Parent":{"$ref":"1"},"Children":[]},{"$id":"17","Id":"1485616302037926929","Parent":{"$ref":"1"},"Children":[]}]}');