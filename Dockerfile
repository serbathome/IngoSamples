FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY sampleapp.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c release -o /app

EXPOSE 80
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "sampleapp.dll"]
