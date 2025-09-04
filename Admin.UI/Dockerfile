FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY Shared.Models/Shared.Models.csproj Shared.Models/
COPY Admin.UI/Admin.UI.csproj Admin.UI/
RUN dotnet restore Admin.UI/Admin.UI.csproj

COPY Shared.Models/ Shared.Models/
COPY Admin.UI/ Admin.UI/

WORKDIR /src/Admin.UI
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
EXPOSE 80
ENTRYPOINT ["dotnet", "Admin.UI.dll"]