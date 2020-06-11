#!/bin/sh

set -e

sass StyleRoot.scss:wwwroot/css/app.css

#gzip -c wwwroot/css/app.css > wwwroot/css/app.css.gz
