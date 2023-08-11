#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Tournament.API/Tournament.API/Tournament.API.csproj", "Services/Tournament.API/Tournament.API/"]
RUN dotnet restore "Services/Tournament.API/Tournament.API/Tournament.API.csproj"
COPY . .
WORKDIR "/src/Services/Tournament.API/Tournament.API"
RUN dotnet build "Tournament.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tournament.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tournament.API.dll"]