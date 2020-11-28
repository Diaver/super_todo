#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS  build-env
WORKDIR /app

# Copy necessary files and restore as distinct layer
COPY ["WebApp/WebApp.csproj", "WebApp/"]
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
HEALTHCHECK --interval=30s --timeout=3s --retries=1 CMD curl --silent --fail http://localhost:7000/hc || exit 1

# Start
ENTRYPOINT ["dotnet", "WebApp.dll"]


#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
#WORKDIR /app
#EXPOSE 7000/tcp
#ENV ASPNETCORE_URLS http://*:7000
#
#FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
#WORKDIR /src
#COPY ["WebApp/WebApp.csproj", "WebApp/"]
#RUN dotnet restore "WebApp/WebApp.csproj"
#COPY . .
#WORKDIR "/src/WebApp"
#RUN dotnet build "WebApp.csproj" -c Release -o /app/build
#
##2 Build client-side artifacts
# 
#FROM node:14.3.0 as clientBuild
#WORKDIR /ClientApp
#COPY "/WebApp/ClientApp/" .
#RUN npm install
#RUN npm run build
#
#FROM build AS publish
#RUN dotnet publish "WebApp.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#COPY --from=clientBuild ./ClientApp/build ./ClientApp/build
#
#ENTRYPOINT ["dotnet", "WebApp.dll"]