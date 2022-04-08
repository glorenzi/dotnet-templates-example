#### Base image
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine as base
WORKDIR /app
EXPOSE 5000

#### Restore and publish with the sdk image
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build

COPY . ./
RUN dotnet publish src/WebApiTemplate/WebApiTemplate.csproj -c Release -o /app/publish

#### Copy app files into the runtime image
FROM base as final

WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "WebApiTemplate.dll"]
