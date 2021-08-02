#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SelfieAWookie.Api.UI/SelfieAWookie.Api.UI.csproj", "SelfieAWookie.Api.UI/"]
RUN dotnet restore "SelfieAWookie.Api.UI/SelfieAWookie.Api.UI.csproj"
COPY . .
WORKDIR "/src/SelfieAWookie.Api.UI"
RUN dotnet build "SelfieAWookie.Api.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SelfieAWookie.Api.UI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SelfieAWookie.Api.UI.dll"]