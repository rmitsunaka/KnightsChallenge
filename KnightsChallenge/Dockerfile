FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["KnightsChallenge.csproj", "./"]  
RUN dotnet restore "KnightsChallenge.csproj"
COPY . .
RUN dotnet build "KnightsChallenge.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KnightsChallenge.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KnightsChallenge.dll"]
