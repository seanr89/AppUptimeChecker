#!/bin/bash
set -e 

#Navigate back a directory to the console app
cd ../

#Trigger a release build for the application
dotnet build -c Release
#Build a docker container locally with the current project
docker build -t uptimechecker .
#Tag the container
docker tag uptimechecker seanraff89/uptimerconsole
#Push the container to my personal hub with latest param
docker push seanraff89/uptimerconsole:latest