#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS  build-env
WORKDIR /app

# Copy necessary files and restore as distinct layer
COPY ["Auth.EventHandler/Auth.EventHandler.csproj", "Auth.EventHandler/"]

COPY ["ApiService.Models/ApiService.Models.csproj", "ApiService.Models/"]
COPY ["Messaging/Messaging.csproj", "Messaging/"]
COPY ["Services/Services.csproj", "Services/"]
COPY ["Auth.Database/Auth.Database.csproj", "Auth.Database/"]

RUN dotnet restore "Auth.EventHandler/Auth.EventHandler.csproj"


# Copy everything else and build
COPY . ./
RUN dotnet publish "Auth.EventHandler/Auth.EventHandler.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim
COPY --from=build-env /app/out .


# Start
ENTRYPOINT ["dotnet", "Auth.EventHandler.dll"]
