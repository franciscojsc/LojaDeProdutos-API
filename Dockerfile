FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

COPY *.sln .
COPY LojaDeProdutos.API/*.csproj ./LojaDeProdutos.API/
RUN dotnet restore

COPY LojaDeProdutos.API/. ./LojaDeProdutos.API/
WORKDIR /source/LojaDeProdutos.API
RUN dotnet publish -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app ./
# Padrão de container ASP.NET
# ENTRYPOINT ["dotnet", "LojaDeProdutos.API.dll"]
# Opção utilizada pelo Heroku
CMD ASPNETCORE_URLS=http://*:$PORT dotnet LojaDeProdutos.API.dll
