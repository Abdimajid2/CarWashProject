FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY Shared.Models/Shared.Models.csproj Shared.Models/
COPY Customer.UI/Customer.UI.csproj Customer.UI/
RUN dotnet restore Customer.UI/Customer.UI.csproj

COPY Shared.Models/ Shared.Models/
COPY Customer.UI/ Customer.UI/

WORKDIR /src/Customer.UI
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
EXPOSE 80
ENTRYPOINT ["dotnet", "Customer.UI.dll"]