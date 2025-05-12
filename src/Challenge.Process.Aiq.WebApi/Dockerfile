FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Challenge.Process.Aiq.WebApi/Challenge.Process.Aiq.WebApi.csproj", "src/Challenge.Process.Aiq.WebApi/"]
COPY ["src/Challenge.Process.Aiq.Domain/Challenge.Process.Aiq.Domain.csproj", "src/Challenge.Process.Aiq.Domain/"]
COPY ["src/Challenge.Process.Aiq.EntityFramework/Challenge.Process.Aiq.EntityFramework.csproj", "src/Challenge.Process.Aiq.EntityFramework/"]
COPY ["src/Challenge.Process.Aiq.Services/Challenge.Process.Aiq.Services.csproj", "src/Challenge.Process.Aiq.Services/"]
RUN dotnet restore "src/Challenge.Process.Aiq.WebApi/Challenge.Process.Aiq.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Challenge.Process.Aiq.WebApi"
RUN dotnet build "Challenge.Process.Aiq.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Challenge.Process.Aiq.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Challenge.Process.Aiq.WebApi.dll"]
