#!/bin/sh

DOCKER_BUILD_TAG=5.0-13
DOCKER_IMAGE=bulzor-demo

set -e

docker build \
    --file "$(pwd)/runtime.Dockerfile" \
    -t us.gcr.io/stablecube/$DOCKER_IMAGE:$DOCKER_BUILD_TAG .

docker push us.gcr.io/stablecube/$DOCKER_IMAGE:$DOCKER_BUILD_TAG
