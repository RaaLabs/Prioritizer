FROM microsoft/dotnet:2.1-sdk AS build-env
WORKDIR /app

VOLUME [ "/app/data" ]

COPY Build/MSBuild/* ./Build/MSBuild/
COPY modules/TimeSeriesPrioritizer/*.csproj ./modules/TimeSeriesPrioritizer/

WORKDIR /app/modules/TimeSeriesPrioritizer

RUN dotnet restore

COPY modules/TimeSeriesPrioritizer/ ./
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.1-runtime-stretch-slim
WORKDIR /app
COPY --from=build-env /app/modules/TimeSeriesPrioritizer/out ./
COPY --from=build-env /app/modules/TimeSeriesPrioritizer/data ./data

RUN useradd -ms /bin/bash moduleuser
USER moduleuser

ENTRYPOINT ["dotnet", "Dolittle.Edge.TimeSeriesPrioritizer.dll"]