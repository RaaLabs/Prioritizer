# Prioritizer

[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=RaaLabs_Prioritizer&metric=coverage)](https://sonarcloud.io/dashboard?id=RaaLabs_Prioritizer)

This document describes the Priortizer module for RaaLabs Edge.

## What does it do?

Priortizer listens to messages received from IdentityMapper with the property `[InputName("events")]` and are producing the events `[OutputName("prioritized")]` and `[OutputName("nonprioritized")]` based on if the timeseries is present in the configuration.

## HowTo

### Configuration

The Prioritizer is configured using a json file like the one below. If an `[InputName("events")]` with the attribute Timeseries contains in the list it will generate prioritized event.  

```json
{
    "prioritized": [
        "c51ff4fc-13ad-4dae-bc78-e82fa2887847",
        "ea63e51d-7d3a-47c4-be17-447b8cb32d0d",
        "9f332679-2e5b-41a8-ab97-585e2d376214",
        "633696dc-bc35-4b7c-9607-e0ea53a90914",
        "437ac116-1328-4fe2-bf10-2dc8b5b67c2b"
    ]
}
```

### IoT Edge Deployment

```json
{
    "content": {
        "modulesContent": {
            "$edgeAgent": {
                "properties.desired.modules.Prioritizer": {
                    "settings": {
                        "image": "<repo-name>/prioritizer:latest",
                        "createOptions": "{\"HostConfig\":{\"Binds\":[\"<mount-path>/prioritizer:/app/data\"]}}"
                    },
                    "type": "docker",
                    "version": "1.0",
                    "status": "running",
                    "restartPolicy": "always"
                },
                "$edgeHub": {
                    "properties.desired.routes.<InputModule>ToPrioritizer": "FROM /messages/modules/<InputModule>/outputs/<outputevent> INTO BrokeredEndpoint(\"/modules/Prioritizer/inputs/events\")",
                    "properties.desired.routes.PrioritizerTo<PrioritizedOutputModule>": "FROM /messages/modules/Prioritizer/outputs/prioritized INTO BrokeredEndpoint(\"/modules/<PrioritizedOutputModule>/inputs/<inputevent>\")",
                    "properties.desired.routes.PrioritizerTo<NonPrioritizedOutputModule>": "FROM /messages/modules/Prioritizer/outputs/nonprioritized INTO BrokeredEndpoint(\"/modules/<NonPrioritizedOutputModule>/inputs/<inputevent>\")"
                }
            }
        }
    }
}
```