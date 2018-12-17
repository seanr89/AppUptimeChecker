#!/bin/bash
set -e 

#Navigate back a directory to the console app
cd ../

#Build a docker container locally with the current project
docker build -t nodesample .
#Tag the container
docker tag nodesample seanraff89/nodesample
#Push the container to my personal hub with latest param
docker push seanraff89/nodesample:latest