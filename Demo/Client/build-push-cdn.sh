#!/bin/bash

set -e

cd "./bin/Debug/netstandard2.1/publish/wwwroot"

gsutil -m rsync -r ./ gs://bulzor.stablecube.com
