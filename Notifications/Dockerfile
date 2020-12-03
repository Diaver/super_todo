FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS  build-env
WORKDIR /app

# Copy necessary files and restore as distinct layer
COPY ["Notifications/Notifications.csproj", "Notifications/"]

COPY ["Messaging/Messaging.csproj", "Messaging/"]

RUN dotnet restore "Notifications/Notifications.csproj"


# Copy everything else and build
COPY . ./
RUN dotnet publish "Notifications/Notifications.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim
COPY --from=build-env /app/out .


# Start
ENTRYPOINT ["dotnet", "Notifications.dll"]
