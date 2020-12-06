#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS  build-env
WORKDIR /app

# Copy necessary files and restore as distinct layer
COPY ["Logging.EventHandler/Logging.EventHandler.csproj", "Logging.EventHandler/"]

COPY ["ApiService.Models/ApiService.Models.csproj", "ApiService.Models/"]
COPY ["Messaging/Messaging.csproj", "Messaging/"]
COPY ["Services/Services.csproj", "Services/"]
COPY ["Logging.Database/Logging.Database.csproj", "Logging.Database/"]

RUN dotnet restore "Logging.EventHandler/Logging.EventHandler.csproj"


# Copy everything else and build
COPY . ./
RUN dotnet publish "Logging.EventHandler/Logging.EventHandler.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim
COPY --from=build-env /app/out .


# Start
ENTRYPOINT ["dotnet", "Logging.EventHandler.dll"]
