#!/bin/bash

cd ../

dotnet build -c Release

docker build -t uptimechecker .

docker tag uptimechecker seanraff89/uptimerconsole

docker push seanraff89/uptimerconsole:latest