# Use the Microsoft's official .NET Core SDK image.
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory inside the container.
WORKDIR /app

# Copy the solution and the csproj files.
COPY "DogBreedIdentification.sln" "./"

COPY "DBI.WebUI/DBI.WebUI.csproj" "DBI.WebUI/"
RUN dotnet restore "DBI.WebUI/DBI.WebUI.csproj"

COPY "DBI.Application/DBI.Application.csproj" "DBI.Application/"
RUN dotnet restore "DBI.Application/DBI.Application.csproj"

COPY "DBI.Domain/DBI.Domain.csproj" "DBI.Domain/"
RUN dotnet restore "DBI.Domain/DBI.Domain.csproj"

COPY "DBI.Infrastructure/DBI.Infrastructure.csproj" "DBI.Infrastructure/"
RUN dotnet restore "DBI.Infrastructure/DBI.Infrastructure.csproj"

# Restore NuGet packages.
#RUN dotnet restore

# Copy everything else and build the application.
COPY . ./
RUN dotnet publish "DBI.WebUI/DBI.WebUI.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app
#COPY mysite.crt /etc/ssl/certs/
#COPY mysite.key /etc/ssl/private/
COPY --from=build /app/out .

# Expose the port your app runs on.
EXPOSE 5252

# Run the application.
ENTRYPOINT ["dotnet", "DBI.WebUI.dll"]