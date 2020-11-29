#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS  build-env
WORKDIR /app

# Copy necessary files and restore as distinct layer
COPY ["TodoTasks.EventHandler/TodoTasks.EventHandler.csproj", "TodoTasks.EventHandler/"]
COPY ["Messaging/Messaging.csproj", "Messaging/"]
COPY ["TodoTasks.Database/TodoTasks.Database.csproj", "TodoTasks.Database/"]
RUN dotnet restore "TodoTasks.Api/TodoTasks.Api.csproj"


# Copy everything else and build
COPY . ./
RUN dotnet publish "TodoTasks.EventHandler/TodoTasks.EventHandler.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim
COPY --from=build-env /app/out .


# Start
ENTRYPOINT ["dotnet", "TodoTasks.EventHandler.dll"]
