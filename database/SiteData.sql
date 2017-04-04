set search_path="dukaan.web";

insert into node_docs(body, type) values ('{"Title":"Home"}', 'Home') returning id;