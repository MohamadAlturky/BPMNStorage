FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.sln ./
COPY ProjectsManagement.Api.Adapters/ProjectsManagement.Api.Adapters.csproj ProjectsManagement.Api.Adapters/
COPY ProjectsManagement.Application/ProjectsManagement.Application.csproj ProjectsManagement.Application/
COPY ProjectsManagement.Contracts/ProjectsManagement.Contracts.csproj ProjectsManagement.Contracts/
COPY ProjectsManagement.Core/ProjectsManagement.Core.csproj ProjectsManagement.Core/
COPY ProjectsManagement.Endpoints.Adapters/ProjectsManagement.Endpoints.Adapters.csproj ProjectsManagement.Endpoints.Adapters/
COPY ProjectsManagement.Identity.Adapters/ProjectsManagement.Identity.Adapters.csproj ProjectsManagement.Identity.Adapters/
COPY ProjectsManagement.IntegrationTests/ProjectsManagement.IntegrationTests.csproj ProjectsManagement.IntegrationTests/
COPY ProjectsManagement.Representer.Adapters/ProjectsManagement.Representer.Adapters.csproj ProjectsManagement.Representer.Adapters/
COPY ProjectsManagement.SharedKernel/ProjectsManagement.SharedKernel.csproj ProjectsManagement.SharedKernel/
COPY ProjectsManagement.Storage.Adapters/ProjectsManagement.Storage.Adapters.csproj ProjectsManagement.Storage.Adapters/
COPY ProjectsManagement.UnitTests/ProjectsManagement.UnitTests.csproj ProjectsManagement.UnitTests/

RUN dotnet restore

COPY . .

RUN dotnet build -c Release -o /app/build

RUN dotnet publish ProjectsManagement.Api.Adapters/ProjectsManagement.Api.Adapters.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ProjectsManagement.Api.Adapters.dll"]
