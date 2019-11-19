FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["ZXVentures.Aplication/ZXVentures.Aplication.csproj", "ZXVentures.Aplication/"]
RUN dotnet restore "ZXVentures.Aplication/ZXVentures.Aplication.csproj"
COPY . .
WORKDIR "/src/ZXVentures.Aplication"
RUN dotnet build "ZXVentures.Aplication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ZXVentures.Aplication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ZXVentures.Aplication.dll"]