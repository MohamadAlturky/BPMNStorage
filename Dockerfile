# Use the official .NET 8 SDK image to build and run tests
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the solution file and necessary project files
COPY *.sln ./
COPY ProjectsManagement.UnitTests/ProjectsManagement.UnitTests.csproj ProjectsManagement.UnitTests/
COPY ProjectsManagement.IntegrationTests/ProjectsManagement.IntegrationTests.csproj ProjectsManagement.IntegrationTests/
COPY ProjectsManagement.Api.Adapters/ProjectsManagement.Api.Adapters.csproj ProjectsManagement.Api.Adapters/
COPY ProjectsManagement.Application/ProjectsManagement.Application.csproj ProjectsManagement.Application/
COPY ProjectsManagement.Contracts/ProjectsManagement.Contracts.csproj ProjectsManagement.Contracts/
COPY ProjectsManagement.Core/ProjectsManagement.Core.csproj ProjectsManagement.Core/
COPY ProjectsManagement.Endpoints.Adapters/ProjectsManagement.Endpoints.Adapters.csproj ProjectsManagement.Endpoints.Adapters/
COPY ProjectsManagement.Identity.Adapters/ProjectsManagement.Identity.Adapters.csproj ProjectsManagement.Identity.Adapters/
COPY ProjectsManagement.Representer.Adapters/ProjectsManagement.Representer.Adapters.csproj ProjectsManagement.Representer.Adapters/
COPY ProjectsManagement.SharedKernel/ProjectsManagement.SharedKernel.csproj ProjectsManagement.SharedKernel/
COPY ProjectsManagement.Storage.Adapters/ProjectsManagement.Storage.Adapters.csproj ProjectsManagement.Storage.Adapters/

# Restore dependencies for all projects
RUN dotnet restore

# Copy all the source code
COPY . .

# Build the solution (this will build all the projects)
RUN dotnet build --no-restore -c Release

# Run the unit tests
RUN dotnet test ProjectsManagement.UnitTests/ProjectsManagement.UnitTests.csproj --no-build --logger:trx

# Run the integration tests
RUN dotnet test ProjectsManagement.IntegrationTests/ProjectsManagement.IntegrationTests.csproj --no-build --logger:trx
