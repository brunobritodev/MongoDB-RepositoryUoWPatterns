FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 56522
EXPOSE 44380

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["MongoDB.GenericRepository/MongoDB.GenericRepository.csproj", "MongoDB.GenericRepository/"]
RUN dotnet restore "MongoDB.GenericRepository/MongoDB.GenericRepository.csproj"
COPY . .
WORKDIR "/src/MongoDB.GenericRepository"
RUN dotnet build "MongoDB.GenericRepository.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MongoDB.GenericRepository.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MongoDB.GenericRepository.dll"]