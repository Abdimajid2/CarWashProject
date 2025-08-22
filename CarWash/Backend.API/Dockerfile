FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ../Shared.Models/Shared.Models.csproj Shared.Models/
COPY Backend.API.csproj Backend.API/
RUN dotnet restore Backend.API/Backend.API.csproj
COPY ../Shared.Models/ Shared.Models/
COPY . Backend.API/
WORKDIR /src/Backend.API
RUN dotnet publish -c Release -o /app
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
EXPOSE 80
ENTRYPOINT ["dotnet", "Backend.API.dll"]