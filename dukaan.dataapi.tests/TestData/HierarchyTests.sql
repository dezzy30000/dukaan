set search_path="dukaan.web";

-- imaginary page types
------------------------
-- crask
-- morbi
-- maecenas 
-- aliquam
-- proin

-- pushing data into node_docs table.

delete from node_docs;

alter table only node_docs alter column id drop default;

insert into node_docs(id, body, type) values (1472459628771017730, '{"Name":"lorem","Body":"Lorem ipsum dolor sit amet, consectetur adipiscing elit."}', 'cras'); 
insert into node_docs(id, body, type) values (1472459628812960771, '{"Name":"ipsum dolor","Body":"Suspendisse a arcu a tortor scelerisque auctor vitae et nisl."}', 'morbi'); 
insert into node_docs(id, body, type) values (1472459628812960772, '{"Name":"sit amet","Body":"Praesent eget turpis at dolor efficitur dapibus nec at augue."}', 'aliquam'); 
insert into node_docs(id, body, type) values (1472459628812960773, '{"Name":"consectetur","Body":"Duis in lorem aliquet, mattis nisi at, finibus urna."}', 'maecenas'); 
insert into node_docs(id, body, type) values (1472459628812960774, '{"Name":"adipiscing","Body":"Curabitur id diam id elit malesuada sollicitudin quis nec velit."}', 'proin'); 
insert into node_docs(id, body, type) values (1472459628812960775, '{"Name":"elit.a","Body":"Mauris ultrices ex et velit condimentum vestibulum vel ac diam."}', 'proin'); 
insert into node_docs(id, body, type) values (1472459628812960776, '{"Name":"Integer sagittis","Body":"Ut et nisi vestibulum, lobortis orci sed, iaculis odio."}', 'aliquam'); 
insert into node_docs(id, body, type) values (1472459628812960777, '{"Name":"condimentum","Body":"Sed fermentum augue at erat dignissim tempor."}', 'cras'); 
insert into node_docs(id, body, type) values (1472459628812960778, '{"Name":"sodales","Body":"Aenean varius neque sit amet diam elementum placerat."}', 'proin'); 
insert into node_docs(id, body, type) values (1472459628812960779, '{"Name":"Pellentesque","Body":"Nam vel felis eu mi faucibus consequat vel non justo."}', 'aliquam'); 
insert into node_docs(id, body, type) values (1472459628812960780, '{"Name":"tempor","Body":"Nam eget massa sed leo suscipit volutpat."}', 'proin'); 
insert into node_docs(id, body, type) values (1472459628812960781, '{"Name":"id","Body":"Ut consectetur ipsum nec venenatis bibendum."}', 'aliquam'); 
insert into node_docs(id, body, type) values (1472459628812960782, '{"Name":"velit","Body":"Donec id felis pharetra sapien efficitur vulputate vel et mauris."}', 'morbi'); 
insert into node_docs(id, body, type) values (1472459628812960783, '{"Name":"eget ullamcorper","Body":"Sed nec erat in nisi porttitor lacinia non et metus."}', 'maecenas'); 
insert into node_docs(id, body, type) values (1472459628821349392, '{"Name":"Vestibulum","Body":"Etiam semper massa non purus bibendum lacinia."}', 'morbi'); 
insert into node_docs(id, body, type) values (1472459628821349393, '{"Name":"tempus","Body":"Pellentesque posuere nunc ut iaculis pulvinar."}', 'maecenas'); 
insert into node_docs(id, body, type) values (1472459628821349394, '{"Name":"dignissim","Body":"Nulla vel orci quis lorem rutrum egestas."}', 'morbi'); 
insert into node_docs(id, body, type) values (1472459628821349395, '{"Name":"lacus","Body":"Donec cursus eros a vestibulum fringilla."}', 'proin'); 
insert into node_docs(id, body, type) values (1472459628821349396, '{"Name":"eget","Body":"Aliquam gravida purus suscipit, tristique odio sit amet, mattis diam."}', 'cras'); 
insert into node_docs(id, body, type) values (1472459628821349397, '{"Name":"dictum","Body":"Pellentesque quis orci ornare, pellentesque sapien eu, varius nibh."}', 'maecenas'); 
insert into node_docs(id, body, type) values (1472459628821349398, '{"Name":"metus","Body":"Vestibulum laoreet elit ac magna finibus dictum."}', 'cras'); 
insert into node_docs(id, body, type) values (1472459628821349399, '{"Name":"finibus","Body":"Ut at erat sed erat venenatis consequat quis eget nulla."}', 'aliquam'); 
insert into node_docs(id, body, type) values (1472459628821349400, '{"Name":"euismod","Body":"Vestibulum ut dolor tincidunt, laoreet nunc bibendum, volutpat ex."}', 'cras'); 
insert into node_docs(id, body, type) values (1472459628821349401, '{"Name":"Maecenas","Body":"Morbi quis neque ac mauris sollicitudin tincidunt et ac mauris."}', 'morbi'); 
insert into node_docs(id, body, type) values (1472459628821349402, '{"Name":"convallis","Body":"Nunc bibendum ex sed massa laoreet, non maximus ex venenatis."}', 'maecenas'); 
insert into node_docs(id, body, type) values (1472459628821349403, '{"Name":"tortor","Body":"Morbi blandit odio ut tellus laoreet, vel imperdiet tortor tristique."}', 'morbi'); 
insert into node_docs(id, body, type) values (1472459628821349404, '{"Name":"at","Body":"Integer vestibulum mi vel auctor ullamcorper."}', 'morbi'); 
insert into node_docs(id, body, type) values (1472459628821349405, '{"Name":"dapibus","Body":"Vivamus sed urna sit amet elit facilisis feugiat."}', 'cras'); 
insert into node_docs(id, body, type) values (1472459628821349406, '{"Name":"viverra","Body":"Aliquam sed felis efficitur, auctor erat sed, porta arcu."}', 'cras'); 
insert into node_docs(id, body, type) values (1472459628829738015, '{"Name":"Mauris","Body":"Duis a lorem nec eros euismod eleifend."}', 'maecenas'); 
insert into node_docs(id, body, type) values (1472459628829738016, '{"Name":"gravida sem","Body":"Curabitur iaculis velit gravida, tempor tellus ullamcorper, ultrices felis."}', 'maecenas'); 
insert into node_docs(id, body, type) values (1472459628829738017, '{"Name":"vel","Body":"Duis non nisi in turpis interdum accumsan."}', 'aliquam'); 
insert into node_docs(id, body, type) values (1472459628829738018, '{"Name":"tellus","Body":"Sed ut purus sit amet mi lacinia bibendum sit amet sit amet augue."}', 'aliquam'); 
insert into node_docs(id, body, type) values (1472459628829738019, '{"Name":"pharetra","Body":"Curabitur a sapien vitae dui rutrum accumsan."}', 'proin'); 
insert into node_docs(id, body, type) values (1472459628829738020, '{"Name":"suscipi","Body":"Integer tempus ipsum mattis auctor hendrerit."}', 'aliquam'); 
insert into node_docs(id, body, type) values (1472459628829738021, '{"Name":"Suspendisse","Body":"Nunc sollicitudin dolor sed nisi luctus, sed tincidunt est scelerisque."}', 'proin'); 
insert into node_docs(id, body, type) values (1472459628829738022, '{"Name":"bibendum","Body":"Proin ultrices ligula eget lacus tempor tincidunt."}', 'morbi'); 
insert into node_docs(id, body, type) values (1472459628829738023, '{"Name":"consectetur","Body":"Proin ut nisl a diam pharetra auctor."}', 'proin'); 
insert into node_docs(id, body, type) values (1472459628829738024, '{"Name":"malesuada","Body":"Vestibulum id lacus ac mi pharetra congue."}', 'maecenas'); 
insert into node_docs(id, body, type) values (1472459628829738025, '{"Name":"aliquet","Body":"Ut facilisis dui a aliquet auctor."}', 'cras');
insert into node_docs(id, body, type) values (1472459628821349499, '{"Name":"massa accumsan","Body":"Praesent sit amet mattis justo, in molestie arcu."}', 'aliquam');

alter table only node_docs alter column id set default id_generator();

-- pushing data into hierarchy_docs table.

delete from hierarchy_docs;

alter table only hierarchy_docs alter column id drop default;

insert into hierarchy_docs(id, key, body) values (1472538848721372202, 'website', '{"Root":{"Id":"1472459628771017730","Children":[{"Id":"1472459628812960771","Children":[{"Id":"1472459628812960772","Children":[{"Id":"1472459628812960773","Children":[{"Id":"1472459628812960774","Children":[{"Id":"1472459628829738023","Children":[{"Id":"1472459628829738024","Children":[{"Id":"1472459628829738025","Children":[]}]}]}]},{"Id":"1472459628812960775","Children":[]},{"Id":"1472459628812960776","Children":[]}]}]},{"Id":"1472459628812960777","Children":[]}]},{"Id":"1472459628812960778","Children":[{"Id":"1472459628812960779","Children":[{"Id":"1472459628812960780","Children":[]},{"Id":"1472459628812960781","Children":[]},{"Id":"1472459628812960782","Children":[]},{"Id":"1472459628812960783","Children":[]},{"Id":"1472459628821349392","Children":[]},{"Id":"1472459628821349393","Children":[]}]}]},{"Id":"1472459628821349394","Children":[{"Id":"1472459628821349395","Children":[{"Id":"1472459628821349396","Children":[{"Id":"1472459628821349397","Children":[]},{"Id":"1472459628821349398","Children":[]},{"Id":"1472459628821349399","Children":[]},{"Id":"1472459628821349400","Children":[]},{"Id":"1472459628821349401","Children":[]},{"Id":"1472459628821349402","Children":[]}]}]}]},{"Id":"1472459628821349499","Children":[{"Id":"1472459628821349403","Children":[]},{"Id":"1472459628821349404","Children":[]},{"Id":"1472459628821349405","Children":[]},{"Id":"1472459628821349406","Children":[]},{"Id":"1472459628829738015","Children":[]},{"Id":"1472459628829738016","Children":[]},{"Id":"1472459628829738017","Children":[]},{"Id":"1472459628829738018","Children":[]},{"Id":"1472459628829738019","Children":[]},{"Id":"1472459628829738020","Children":[]},{"Id":"1472459628829738021","Children":[]},{"Id":"1472459628829738022","Children":[]}]}]}}');

alter table only hierarchy_docs alter column id set default id_generator();