\set password `cat /run/secrets/db_dukaan_web_user_password`

create USER dukaan_web with
	login
	superuser
	createdb
	createrole
	inherit
	noreplication
	connection limit -1
	password :'password';