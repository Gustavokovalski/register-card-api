# RegisterCard API
## Overview
This is a .NET 8 API project designed with Clean Architecture principles, implementing CQRS (Command Query Responsibility Segregation) pattern, and utilizing the Strategy and Abstract Factory design patterns for token providers. The project is fully tested with 100% code coverage and deployed to Google Cloud Platform (GCP).

- [Published App in GCP (Google Cloud Plataform)](https://register-card-api-784529591133.us-central1.run.app/swagger/index.html) 

- **100% Code Coverage**
![image](https://github.com/user-attachments/assets/7f42b3f7-4bce-4eea-817a-9ee4f59cb25c)
![image](https://github.com/user-attachments/assets/bf75f05d-d435-4660-8e22-decb0ddff98a)


## How to test the application
You can access a published API and test directly through swagger
[Published App in GCP (Google Cloud Plataform)](https://register-card-api-784529591133.us-central1.run.app/swagger/index.html) 

**To __test__ the application, in the project root folder, enter the command below and press enter:**

- **Run project (cmd)**

```powershell
> dotnet run --project WebApi/WebApi.csproj
```

access the api on the port https://localhost:7147/swagger/index.html

**Run all Tests (cmd)**

```powershell
> dotnet test .\RegisterCard.sln
```

