#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/ProcessManager/ProcessManager.API/ProcessManager.API.csproj", "Services/ProcessManager/ProcessManager.API/"]
RUN dotnet restore "Services/ProcessManager/ProcessManager.API/ProcessManager.API.csproj"
COPY . .
WORKDIR "/src/Services/ProcessManager/ProcessManager.API"
RUN dotnet build "ProcessManager.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProcessManager.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProcessManager.API.dll"]
