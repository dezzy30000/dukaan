create extension if not exists plv8;

drop schema if exists "dukaan.web" cascade;

create schema "dukaan.web";

set search_path="dukaan.web";

drop sequence if exists global_id_seq;
create sequence global_id_seq;

create function id_generator(out result bigint) AS $$
declare
    our_epoch bigint := 1314220021721;
    seq_id bigint;
    now_millis bigint;
    shard_id int := 1;
begin
	select nextval('global_id_seq')%1024 into seq_id;
    select floor(extract(EPOCH from clock_timestamp()) * 1000) into now_millis;
    result := (now_millis - our_epoch) << 23;
    result := result | (shard_id << 10);
    result := result | (seq_id);
end;
$$ language plpgsql;

create function get_hierarchy_ids(hierarchy_key varchar)
returns varchar[] as $$ 
    var hierarchy = plv8.execute("select body from hierarchy_docs where key = $1;", hierarchy_key)
	
	if(hierarchy.length == 0){
		return [];
	}
	
	hierarchy = hierarchy[0];

    var ids = [];
    
	(function(root, callback){
		(function recurse(node) {
			for (var index = 0, length = node.Children.length; index < length; index++) {
				recurse(node.Children[index]);
			}
			callback(node);
		})(root);
	}(hierarchy.body.Root, function(node){
      	ids.push(node.Id);
    }));
      
	return ids;      
$$ language plv8;

create function get_hierarchy(hierarchy_key varchar) 
returns jsonb as $$
    var hierarchy = plv8.execute("select id::varchar, key, body, created_at, updated_at, (select json_agg(node_doc) from (select id::varchar, body, type, created_at, updated_at from node_docs where id = any (get_hierarchy_ids($1)::bigint[])) as node_doc) as hierarchy_data from hierarchy_docs where key = $1;", hierarchy_key);

	if(hierarchy.length == 0){
		return {};
	}

	hierarchy = hierarchy[0];
	    
    var tree = {
    	"Id" : hierarchy.id,
        "Key" : hierarchy.key,
        "Body" : hierarchy.body,
        "CreatedAt" : hierarchy["created_at"],
        "UpdatedAt" : hierarchy["updated_at"]
    };
    
    var hierarchyData = hierarchy["hierarchy_data"];
        
	(function(root, callback){
		(function recurse(node) {
			for (var index = 0, length = node.Children.length; index < length; index++) {
				recurse(node.Children[index]);
			}
			callback(node);
		})(root);
	}(tree.Body.Root, function(node){
      	var found = false;
      
		for(var index = 0; index < hierarchyData.length; index++){      
      		if(hierarchyData[index].id === node.Id){
      			node.Content = hierarchyData[index].body;
        		node.Type = hierarchyData[index].type;
        		node["CreatedAt"] = hierarchyData[index]["created_at"];
        		node["UpdatedAt"] = hierarchyData[index]["updated_at"];
      			found = true;
      			break;
      		}
      	}
      
      	if(!found){
      		plv8.elog(WARNING, "Couldn not find record for node with id " + node.id + " and for hierarchy with key value " + hierarchy_key);
      	}
    }));
    
    return tree;
$$ language plv8;

create table node_docs(
	id bigint primary key not null unique default id_generator(),
    body jsonb not null,
    type varchar(100) not null,
    created_at timestamptz default now() not null,
    updated_at timestamptz default now() not null);
    
create table hierarchy_docs(
    id bigint primary key not null unique default id_generator(),
    key varchar(100) not null unique,    
    body jsonb not null,
    created_at timestamptz default now() not null,
    updated_at timestamptz default now() not null);

create unique index hierarchy_docs_key_unique on hierarchy_docs(key);