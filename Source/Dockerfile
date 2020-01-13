FROM microsoft/dotnet:2.1-sdk AS build-env
WORKDIR /app

COPY default.props ./default.props
COPY Source/*.csproj ./Source/

WORKDIR /app/Source

RUN dotnet restore

COPY Source/ ./
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.1-runtime-stretch-slim
WORKDIR /app
COPY --from=build-env /app/Source/out ./
COPY --from=build-env /app/Source/data ./data

RUN useradd -ms /bin/bash moduleuser
USER moduleuser

ENTRYPOINT ["dotnet", "RaaLabs.TimeSeries.Prioritizer.dll"]