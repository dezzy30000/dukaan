# dukaan

A Content Management System designed from the ground to be fast and scalable.

To run the application

1. Install Docker

2. Edit the files in the secrets folder by replacing the text `<password>` with your chosen password. (I know you are not supposed to commit secrets into the repository, but this is a proof of concept).

3. Type and the enter the command `docker-compose up` from the root of the application directory.

4. Wait for things to spin up and then navigate to `http://localhost:51212/`

Technologies

- asp.net core 2.1
- postgresql
- docker
