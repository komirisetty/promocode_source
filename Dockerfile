FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
COPY . /myapp
WORKDIR /myapp
RUN dotnet restore
RUN dotnet publish -c Release -o myout -r linux-x64
FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS runtime
WORKDIR /myapp
COPY --from=build /myapp/myout ./
ENV ASPNETCORE_ENVIRONMENT Production
ENTRYPOINT ["dotnet", "Alphasource.Hosting.Promocodes.dll"]
