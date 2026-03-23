# Use the official .NET 10.0 SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy the solution file and project files
COPY ["PocketCard.sln", "."]
COPY ["PocketCard.Api/PocketCard.Api.csproj", "PocketCard.Api/"]
COPY ["PocketCard.Domain/PocketCard.Domain.csproj", "PocketCard.Domain/"]
COPY ["PocketCard.UseCases/PocketCard.UseCases.csproj", "PocketCard.UseCases/"]
COPY ["PocketCard.Infrastructure/PocketCard.Infrastructure.csproj", "PocketCard.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "PocketCard.Api/PocketCard.Api.csproj"

# Copy the entire source code
COPY ["PocketCard.Api/", "PocketCard.Api/"]
COPY ["PocketCard.Domain/", "PocketCard.Domain/"]
COPY ["PocketCard.UseCases/", "PocketCard.UseCases/"]
COPY ["PocketCard.Infrastructure/", "PocketCard.Infrastructure/"]

# Build the application
WORKDIR "/src/PocketCard.Api"
RUN dotnet build "PocketCard.Api.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "PocketCard.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the official .NET 10.0 ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Create a non-root user
RUN adduser --disabled-password --gecos '' appuser && chown -R appuser:appuser /app
USER appuser

# Copy the published application
COPY --from=publish /app/publish .

# Expose the port the app runs on
EXPOSE 8080

# Set the entry point
ENTRYPOINT ["dotnet", "PocketCard.Api.dll"]