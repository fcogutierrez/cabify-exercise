FROM mcr.microsoft.com/dotnet/sdk:6.0

WORKDIR /app
COPY /publish .

EXPOSE 9091/tcp

ENV ASPNETCORE_URLS "http://*:9091"
ENV ASPNETCORE_ENVIRONMENT "$ENVIRONMENT"
CMD ["dotnet", "Cabify.CarPooling.Api.dll"]