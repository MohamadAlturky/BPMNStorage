# Use the official .NET 8 SDK image to build and publish the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the solution file and restore the dependencies
COPY *.sln ./
COPY ProjectsManagement.Api.Adapters/ProjectsManagement.Api.Adapters.csproj ProjectsManagement.Api.Adapters/
RUN dotnet restore

# Copy the rest of the application code
COPY . .

# Build and publish the application
RUN dotnet publish ProjectsManagement.Api.Adapters/ProjectsManagement.Api.Adapters.csproj -c Release -o out

# Use the official .NET 8 runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory for the application
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /app/out .

# Expose the port your API will run on
EXPOSE 80

# Set the entry point for the Docker container
ENTRYPOINT ["dotnet", "ProjectsManagement.Api.Adapters.dll"]
