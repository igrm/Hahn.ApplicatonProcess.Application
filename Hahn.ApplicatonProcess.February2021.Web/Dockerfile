#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM node:14.15.5-alpine AS client
WORKDIR /app 
COPY ["Hahn.ApplicatonProcess.February2021.Web", "."]
WORKDIR /app/ClientApp
RUN echo $(npm cache clean -f) 
RUN echo $(npm install)
RUN echo $(npm run build)

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Hahn.ApplicatonProcess.February2021.Web/Hahn.ApplicatonProcess.February2021.Web.csproj", "Hahn.ApplicatonProcess.February2021.Web/"]
COPY ["Hahn.ApplicatonProcess.February2021.Data/Hahn.ApplicatonProcess.February2021.Data.csproj", "Hahn.ApplicatonProcess.February2021.Data/"]
COPY ["Hahn.ApplicatonProcess.February2021.Domain/Hahn.ApplicatonProcess.February2021.Domain.csproj", "Hahn.ApplicatonProcess.February2021.Domain/"]
RUN dotnet restore "Hahn.ApplicatonProcess.February2021.Web/Hahn.ApplicatonProcess.February2021.Web.csproj"
COPY . .
WORKDIR "/src/Hahn.ApplicatonProcess.February2021.Web"
RUN dotnet build "Hahn.ApplicatonProcess.February2021.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hahn.ApplicatonProcess.February2021.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=client /app/wwwroot ./wwwroot
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hahn.ApplicatonProcess.February2021.Web.dll"]