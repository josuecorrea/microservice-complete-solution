#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Product.Service/Product.Service.csproj", "Product.Service/"]
RUN dotnet restore "Product.Service/Product.Service.csproj"
COPY . .
WORKDIR "/src/Product.Service"
RUN dotnet build "Product.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Product.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Product.Service.dll"]