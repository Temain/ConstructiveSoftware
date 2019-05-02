FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["ConstructiveSoftware.WebApi/ConstructiveSoftware.WebApi.csproj", "ConstructiveSoftware.WebApi/"]
RUN dotnet restore "ConstructiveSoftware.WebApi/ConstructiveSoftware.WebApi.csproj"
COPY . .
WORKDIR "/src/ConstructiveSoftware.WebApi"
RUN dotnet build "ConstructiveSoftware.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ConstructiveSoftware.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ConstructiveSoftware.WebApi.dll"]