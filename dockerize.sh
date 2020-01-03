#!/bin/bash
export VERSION=$(git tag --sort=-version:refname | head -1)
docker build --no-cache -f ./Source/Dockerfile -t shipos/timeseries-prioritizer:$VERSION .
docker push shipos/timeseries-prioritizer:$VERSION