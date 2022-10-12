FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
WORKDIR "/src"
ENTRYPOINT ["dotnet", "test", "StoneDesafio.Tests/StoneDesafio.Tests.csproj"]