#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Authetication.Service/Authetication.Service.csproj", "Authetication.Service/"]
RUN dotnet restore "Authetication.Service/Authetication.Service.csproj"
COPY . .
WORKDIR "/src/Authetication.Service"
RUN dotnet build "Authetication.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Authetication.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Authetication.Service.dll"]