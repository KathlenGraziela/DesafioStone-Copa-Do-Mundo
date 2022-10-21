FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["StoneDesafio/StoneDesafio.csproj", "StoneDesafio/"]
RUN dotnet restore "StoneDesafio/StoneDesafio.csproj"
COPY . .
WORKDIR "/src/StoneDesafio"
RUN dotnet build "StoneDesafio.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StoneDesafio.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StoneDesafio.dll"]