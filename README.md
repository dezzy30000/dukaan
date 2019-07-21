# dukaan

A Content Management System designed from the ground up to be fast, scalable and platform agnostic.

To run the application

1. Install Docker and Docker Compose.

2. Type and the enter the command `sudo docker-compose up` from the root of the application directory.

3. Wait for things to spin up and then navigate to `http://localhost:51212/`.

4. Want to administer, see the schema or data in the database?

    - Navigate to `http://localhost:5050/`.
    - Login with the username `mohammad.shahab.rafiq@dukaan.co.uk` and password `{%096t4y8f99<@I`.
    - Bring up the context menu on the `Servers` node, hover over the `Create` and click the `Server` node.
    - Enter a name for the connection on `General` tab and then move over to the `Connection` tab.
    - Enter `database` in the `Host name/address` input field.
    - Enter `dukaan_web` in the `Username` input field.
    - Enter `;Ncd660(83m3V8F` in the `Password` input field, optionally check save password and then click Save.

Technologies

- [asp.net core 2.1](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-2.1)
- [postgresql](https://www.postgresql.org/)
- [docker](https://www.docker.com/)
- [plv8](https://github.com/plv8/plv8)
- [pgadmin](https://www.pgadmin.org/)

Nitpick corner

I know you are not supposed to commit secrets into the repository, but this is a proof of concept.
