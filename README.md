# Prioritizer

[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=RaaLabs_Prioritizer&metric=coverage)](https://sonarcloud.io/dashboard?id=RaaLabs_Prioritizer)

This document describes the Priortizer module for RaaLabs Edge.

## What does it do?

Priortizer listens to messages received from IdentityMapper with the property `[InputName("events")]` and are producing the events `[OutputName("prioritized")]` and `[OutputName("nonprioritized")]` based on if the timeseries is present in the configuration.


## Configuration

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

## IoT Edge Deployment


### $edgeAgent
In your `deployment.json` file, you will need to add the module. For more details on modules in IoT Edge, go [here](https://docs.microsoft.com/en-us/azure/iot-edge/module-composition).

The module depends has persistent state and it is assuming that this is in the `data` folder relative to where the binary is running.
Since this is running in a containerized environment, the state is not persistent between runs. To get this state persistent, you'll
need to configure the deployment to mount a folder on the host into the data folder.

In your `deployment.json` file where you added the module, inside the `HostConfig` property, you should add the
volume binding.

```json
"Binds": [
    "<mount-path>:/app/data"
]
```

```json
{
    "modulesContent": {
        "$edgeAgent": {
            "properties.desired.modules.Prioritizer": {
                "settings": {
                    "image": "<repo-name>/prioritizer:<tag>",
                    "createOptions": "{\"HostConfig\":{\"Binds\":[\"<mount-path>:/app/data\"]}}"
                },
                "type": "docker",
                "version": "1.0",
                "status": "running",
                "restartPolicy": "always"
            }
        }
    }
}
```

For production setup the bind mount can be replaced by a docker volume.

### $edgeHub

The routes in edgeHub can be specified like the example below. The *prioritized* events are sent to *PrioritizedModule* and *nonprioritized* events are sent to *NonPrioritizedModule*. 

```json
{
    "$edgeHub": {
        "properties.desired.routes.<InputModule>ToPrioritizer": "FROM /messages/modules/<InputModule>/outputs/<outputevent> INTO BrokeredEndpoint(\"/modules/Prioritizer/inputs/events\")",
        "properties.desired.routes.PrioritizerTo<PrioritizedModule>": "FROM /messages/modules/Prioritizer/outputs/prioritized INTO BrokeredEndpoint(\"/modules/<PrioritizedModule>/inputs/<inputevent>\")",
        "properties.desired.routes.PrioritizerTo<NonPrioritizedModule>": "FROM /messages/modules/Prioritizer/outputs/nonprioritized INTO BrokeredEndpoint(\"/modules/<NonPrioritizedModule>/inputs/<inputevent>\")"
    }
}
```
