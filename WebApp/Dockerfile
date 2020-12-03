
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS  build-env
WORKDIR /app

# Copy necessary files and restore as distinct layer
COPY ["WebApp/WebApp.csproj", "WebApp/"]
COPY ["ApiService.Interfaces/ApiService.Interfaces.csproj", "ApiService.Interfaces/"]
COPY ["ApiService.Models/ApiService.Models.csproj", "ApiService.Models/"]
RUN dotnet restore "WebApp/WebApp.csproj"


# Copy everything else and build
COPY . ./
RUN dotnet publish "WebApp/WebApp.csproj" -c Release -o out

#2 Build client-side artifacts
 
FROM node:14.3.0 as clientBuild
WORKDIR /ClientApp
COPY "/WebApp/ClientApp/" .
RUN npm install
RUN npm run build

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim
COPY --from=build-env /app/out .
COPY --from=clientBuild ./ClientApp/build ./ClientApp/build

# Expose ports
EXPOSE 7000/tcp
ENV ASPNETCORE_URLS http://*:7000

# Start
ENTRYPOINT ["dotnet", "WebApp.dll"]