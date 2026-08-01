FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base

WORKDIR /app
EXPOSE 8080
EXPOSE 443

LABEL maintainer="PoupeMoney" description="PoupeMoney Core WebApi"

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

COPY . .

WORKDIR /src

RUN dotnet restore "PoupeMoney.Core.WebApi/PoupeMoney.Core.WebApi.csproj"

WORKDIR "/src/PoupeMoney.Core.WebApi"

RUN dotnet build "PoupeMoney.Core.WebApi.csproj" -c Release -o /app/build

FROM build AS tests
RUN dotnet test

FROM build AS publish
RUN dotnet publish "PoupeMoney.Core.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PoupeMoney.Core.WebApi.dll"]
