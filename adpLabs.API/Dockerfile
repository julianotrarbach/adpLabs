#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["adpLabs.API/adpLabs.API.csproj", "adpLabs.API/"]
RUN dotnet restore "adpLabs.API/adpLabs.API.csproj"
COPY . .
WORKDIR "/src/adpLabs.API"
RUN dotnet build "adpLabs.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "adpLabs.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "adpLabs.API.dll"]