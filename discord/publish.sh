#!/bin/bash
sudo systemctl stop basil-discord-service
dotnet publish -r linux-x64 -o ./pub
sudo systemctl start basil-discord-service