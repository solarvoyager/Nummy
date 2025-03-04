## Nummy 💎

Nummy is a web application that shows your applications' logs and exceptions, request & response logs in one place using theese packages:

[Nummy.CodeLogger](https://www.nuget.org/packages/Nummy.CodeLogger) - to log any information you need

[Nummy.ExceptionHandler](https://www.nuget.org/packages/Nummy.ExceptionHandler) - to handle exceptions correctly

[Nummy.HttpLogger](https://www.nuget.org/packages/Nummy.HttpLogger) - to log http requests and responses

### Screens

![Dashboard](NummyUi/wwwroot/screens/dashboard.png)
![CodeLogs](NummyUi/wwwroot/screens/code_logs.png)


It is built using .NET Core, Entity Framework Core, and PostgreSQL.

### To set up Nummy on your Docker:

1. Copy [docker-compose.yml](https://github.com/solarvoyager/Nummy/blob/fb5247f0b977d1d20424abc4c87f8a1c0d621bcd/docker-compose.yml) file.
2. Run local instance using: `docker compose up --detach`
3. Open browser and go to Dashboard http://localhost:8080/
4. Copy DSN address from UI and configure it to your .net core application.

