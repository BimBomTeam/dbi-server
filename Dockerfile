# Use the Microsoft's official .NET Core SDK image.
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory inside the container.
WORKDIR /app

# Copy the solution and the csproj files.
COPY ["DBI.sln", "./"]
COPY ["DBI.WebUI/DBI.WebUI.csproj", "DBI.WebUI/"]
COPY ["DBI.Application/DBI.Application.csproj", "DBI.Application/"]
COPY ["DBI.Domain/DBI.Domain.csproj", "DBI.Domain/"]
COPY ["DBI.Infrastructure/DBI.Infrastructure.csproj", "DBI.Infrastructure/"]

# Restore NuGet packages.
RUN dotnet restore

# Copy everything else and build the application.
COPY . ./
RUN dotnet publish "DBI.WebUI/DBI.WebUI.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .

# Expose the port your app runs on.
EXPOSE 80

# Run the application.
ENTRYPOINT ["dotnet", "DBI.WebUI.dll"]