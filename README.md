# :eyes: Overview

This repository contains a web app for working with events and registering participants for these events.
The application can be launched by following the instructions below.

## Startup instructions

> [!NOTE]
> This method uses Docker, so make sure you have Docker and Docker Compose installed on your machine.

1. Clone repository:

```bash
git clone https://github.com/anticlown322/Events-Web-Application
```

2. Go to the folder with docker file

```bash
cd Events-Web-Application/backend
```

3. Run docker compose for building and starting containers

```bash
docker-compose up -d
```

Option `-d` Allows you to run containers in the background.

4. Once the containers have been successfully launched, the application will be available at:

```bash
http://localhost:8080/swagger/index.html
```

## First steps

1. Create an admin using `api\authenticate` POST request in Swagger.

Once admin has been successfully created, you will receive an access token.
Authorize with it using Swagger `Authorize` button.

2. Send some requests(i.e. create an event).

3. If you need to stop the containers, run the command:

```bash
docker-compose down
```
