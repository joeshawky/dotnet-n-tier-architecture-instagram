FROM mcr.microsoft.com/dotnet/sdk:6.0-focal as build
WORKDIR /source
COPY . .
RUN dotnet restore "./UiLayerMvc/UiLayerMvc.csproj"
RUN dotnet publish "./UiLayerMvc/UiLayerMvc.csproj" -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
WORKDIR /app
COPY --from=build /app ./

EXPOSE 80

ENTRYPOINT [ "dotnet", "UiLayerMvc.dll" ]