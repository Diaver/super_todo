#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS  build-env
WORKDIR /app

# Copy necessary files and restore as distinct layer
COPY ["TodoTasks.Api/TodoTasks.Api.csproj", "TodoTasks.Api/"]
COPY ["TodoTasks.Database/TodoTasks.Database.csproj", "TodoTasks.Database/"]
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
EXPOSE 7100/tcp
ENV ASPNETCORE_URLS http://*:7100

# Start
ENTRYPOINT ["dotnet", "TodoTasks.Api.dll"]
