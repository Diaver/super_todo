#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS  build-env
WORKDIR /app

# Copy necessary files and restore as distinct layer
COPY ["TodoTasks.Api/TodoTasks.Api.csproj", "TodoTasks.Api/"]
COPY ["ApiService.Interfaces/ApiService.Interfaces.csproj", "ApiService.Interfaces/"]
COPY ["ApiService.Models/ApiService.Models.csproj", "ApiService.Models/"]
COPY ["Services/Services.csproj", "Services/"]
RUN dotnet restore "TodoTasks.Api/TodoTasks.Api.csproj"


# Copy everything else and build
COPY . ./
RUN dotnet publish "TodoTasks.Api/TodoTasks.Api.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim
COPY --from=build-env /app/out .

# Expose ports
EXPOSE 5000/tcp
ENV ASPNETCORE_URLS http://*:5000
HEALTHCHECK --interval=30s --timeout=3s --retries=1 CMD curl --silent --fail http://localhost:5000/hc || exit 1

# Start
ENTRYPOINT ["dotnet", "TodoTasks.Api.dll"]
