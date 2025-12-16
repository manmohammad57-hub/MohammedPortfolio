# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy only csproj first (improves caching)
COPY MohammedPortfolio.csproj ./
RUN dotnet restore

# Copy the rest
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app

# Set environment as Docker
ENV ASPNETCORE_ENVIRONMENT=Docker

# Expose port
EXPOSE 8080
ENV ASPNETCORE_URLS=http://0.0.0.0:8080

# Copy published app
COPY --from=build /app/publish ./

ENTRYPOINT ["dotnet", "MohammedPortfolio.dll"]
