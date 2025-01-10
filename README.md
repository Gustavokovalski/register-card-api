# RegisterCard

## Overview

Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.

## How to test the application

Before starting the tests, it is necessary to run an image of the database in docker, so in the project root folder, enter the command below and press enter:

```powershell
> docker compose up -d
```

```information
Note: the database image is only needed for the application integration tests
```

To __test__ the application, in the project root folder, enter the command below and press enter:

...to run all tests

```powershell
> dotnet test .\RegisterCard.sln
```

...to run only application integration tests

```powershell
> dotnet test .\tests\Application.IntegrationTests\
```

...to run only application logic unit tests

```powershell
> dotnet test .\tests\Application.UnitTests\
```

At the end of the tests do not forget to run the command below and press enter

```powershell
> docker compose down
```

