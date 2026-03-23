FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src


COPY ["PocketCard.sln", "."]
COPY ["PocketCard.Api/PocketCard.Api.csproj", "PocketCard.Api/"]
COPY ["PocketCard.Domain/PocketCard.Domain.csproj", "PocketCard.Domain/"]
COPY ["PocketCard.UseCases/PocketCard.UseCases.csproj", "PocketCard.UseCases/"]
COPY ["PocketCard.Infrastructure/PocketCard.Infrastructure.csproj", "PocketCard.Infrastructure/"]


RUN dotnet restore "PocketCard.Api/PocketCard.Api.csproj"

COPY ["PocketCard.Api/", "PocketCard.Api/"]
COPY ["PocketCard.Domain/", "PocketCard.Domain/"]
COPY ["PocketCard.UseCases/", "PocketCard.UseCases/"]
COPY ["PocketCard.Infrastructure/", "PocketCard.Infrastructure/"]


WORKDIR "/src/PocketCard.Api"
RUN dotnet build "PocketCard.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PocketCard.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

#RUN adduser --disabled-password --gecos '' appuser && chown -R appuser:appuser /app
#USER appuser

COPY --from=publish /app/publish .

EXPOSE 5225

ENTRYPOINT ["dotnet", "PocketCard.Api.dll"]