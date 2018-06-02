#!/bin/bash

export ASPNETCORE_ENVIRONMENT="Production"
export ASPNETCORE_URLS="http://*:5000"

screen -dmS newsite_v1 dotnet ./MathSite.dll