#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS  build-env
WORKDIR /app

# Copy necessary files and restore as distinct layer
COPY ["ApiService.Interfaces/ApiService.Interfaces.csproj", "ApiService.Interfaces/"]
COPY ["ApiService.Models/ApiService.Models.csproj", "ApiService.Models/"]
COPY ["Messaging/Messaging.csproj", "Messaging/"]
COPY ["Services/Services.csproj", "Services/"]
COPY ["Users.Api/Users.Api.csproj", "Users.Api/"]
COPY ["Users.Database/Users.Database.csproj", "Users.Database/"]

RUN dotnet restore "Users.Api/Users.Api.csproj"


# Copy everything else and build
COPY . ./
RUN dotnet publish "Users.Api/Users.Api.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim
COPY --from=build-env /app/out .

# Expose ports
EXPOSE 7200/tcp
ENV ASPNETCORE_URLS http://*:7200

# Start
ENTRYPOINT ["dotnet", "Users.Api.dll"]
