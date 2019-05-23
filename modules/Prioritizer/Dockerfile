FROM microsoft/dotnet:2.1-sdk AS build-env
WORKDIR /app

VOLUME [ "/app/data" ]

COPY Build/MSBuild/* ./Build/MSBuild/
COPY modules/Prioritizer/*.csproj ./modules/Prioritizer/

WORKDIR /app/modules/Prioritizer

RUN dotnet restore

COPY modules/Prioritizer/ ./
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.1-runtime-stretch-slim
WORKDIR /app
COPY --from=build-env /app/modules/Prioritizer/out ./
COPY --from=build-env /app/modules/Prioritizer/data ./data

RUN useradd -ms /bin/bash moduleuser
USER moduleuser

ENTRYPOINT ["dotnet", "Dolittle.TimeSeries.Prioritizer.dll"]