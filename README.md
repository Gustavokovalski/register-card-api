# RegisterCard API
## Overview
This is a .NET 8 API project designed with Clean Architecture principles, implementing CQRS (Command Query Responsibility Segregation) pattern, and utilizing the Strategy and Abstract Factory design patterns for token providers. The project is fully tested with 100% code coverage and deployed to Google Cloud Platform (GCP).

- [Published App in GCP (Google Cloud Plataform)](https://register-card-api-784529591133.us-central1.run.app/swagger/index.html) 

- **100% Code Coverage**
![Private picture]![image](https://github.com/user-attachments/assets/7f42b3f7-4bce-4eea-817a-9ee4f59cb25c)
![Private picture]![image](https://github.com/user-attachments/assets/bf75f05d-d435-4660-8e22-decb0ddff98a)


## How to test the application

There are 3 options for testing

**You can access a published API and test directly through swagger**
[Published App in GCP (Google Cloud Plataform)](https://register-card-api-784529591133.us-central1.run.app/swagger/index.html) 

**or in the project root folder, enter the command below and press enter:**

- **Run project (cmd)**

```powershell
> dotnet run --project WebApi/WebApi.csproj
```

access the api on the port https://localhost:7147/swagger/index.html

**Run all Tests (cmd)**

```powershell
> dotnet test .\RegisterCard.sln
```

**or for test the application using Docker, in the project root folder, type the command below and press enter:**

```powershell
> docker build -t register-card:latest .
```

```powershell
>  docker run -d -p 8080:8080 --name register-card register-card:latest .
```

```powershell
>  docker logs register-card
```

![Private picture]![image](https://github.com/user-attachments/assets/a9c0f9f4-9c54-4643-9b33-0278ff055e03)

access the api on the port http://localhost:8080/swagger/index.html





