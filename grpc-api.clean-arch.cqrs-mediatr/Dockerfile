# .NET 9.0 Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["src/Exp.TodoApp.GrpcApi/Exp.TodoApp.GrpcApi.csproj", "Exp.TodoApp.GrpcApi/"]
RUN dotnet restore "Exp.TodoApp.GrpcApi/Exp.TodoApp.GrpcApi.csproj"
COPY . .
WORKDIR "/src/Exp.TodoApp.GrpcApi"
RUN dotnet build "Exp.TodoApp.GrpcApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Exp.TodoApp.GrpcApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Exp.TodoApp.GrpcApi.dll"]
