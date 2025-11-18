# Use the official .NET 9.0 runtime as base image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["AttendanceTracker.csproj", "."]
RUN dotnet restore "AttendanceTracker.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "AttendanceTracker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AttendanceTracker.csproj" -c Release -o /app/publish

# Final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AttendanceTracker.dll"]