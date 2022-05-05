FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Sample.DockerContainers/Sample.DockerContainers.csproj", "Sample.DockerContainers/"]
RUN dotnet restore "Sample.DockerContainers/Sample.DockerContainers.csproj"
COPY . .
WORKDIR "/src/Sample.DockerContainers"
RUN dotnet build "Sample.DockerContainers.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sample.DockerContainers.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sample.DockerContainers.dll"]
