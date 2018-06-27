\set password `echo "$DUKAAN_WEB_USER_PASSWORD_FILE"`

create USER dukaan_web with
	login
	superuser
	createdb
	createrole
	inherit
	noreplication
	connection limit -1
	password :'password';