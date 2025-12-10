# Use the official .NET 9 SDK image to build and publish the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY ["EmployeeManagemenSystem.csproj", "./"]
RUN dotnet restore "EmployeeManagemenSystem.csproj"

# Copy the remaining source code and publish the app
COPY . .
RUN dotnet publish "EmployeeManagemenSystem.csproj" -c Release -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Expose port (default for .NET 8+ is 8080)
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "EmployeeManagemenSystem.dll"]
