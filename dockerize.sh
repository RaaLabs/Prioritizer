#!/bin/bash
docker build --no-cache -f ./Source/Dockerfile -t raaedge.azurecr.io/prioritizer:test .
docker push raaedge.azurecr.io/prioritizer:test