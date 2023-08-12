#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Logger/Logger.API/Logger.API.csproj", "Services/Logger/Logger.API/"]
RUN dotnet restore "Services/Logger/Logger.API/Logger.API.csproj"
COPY . .
WORKDIR "/src/Services/Logger/Logger.API"
ENV ConnectionStrings__YourDatabaseConnection="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AppLog;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"
RUN dotnet build "Logger.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Logger.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Logger.API.dll"]