# Simple Instrument System

## Purpose 

This project demonstrate how a simple distributed instrument system could be implemented in C# over the .Net framework

## Structure

### ClientServer 
Contains a Blazor WebAssembly client app hosted on nginx and ASP.NET Core server app hosted in ASP.NET Core.
a docker-compose file hold the two following project together and start their respectives images.

[docker-compose.yml](/ClientServer/docker-compose.yml)

#### Client
Client Project - uses nginx on alpine base image. Blazor is the front-end language. 

#### Server 
Server Project- uses ASP.NET runtime image

### HardwareModule
Contains a ASP.NET Core server hosting a REST API allowing to control a similated hardware. Reuse this part if you need to implement a real hardware module. 

### DB 
Pull a standard docker images for Postgres and initialize it. Command to the hardware module and state changes are logged to the DB


## Installation 
- Target environment : Ubuntu 20.04 Long Term Support
- Prerequisite : Docker 
https://www.digitalocean.com/community/tutorials/how-to-install-and-use-docker-on-ubuntu-20-04
Be sure to turn off VPN connection while installing the docker daemon.
- docker-compose 
    sudo apt  install docker-compose


Resources :
https://github.com/dotnet/dotnet-docker .Net related images supported by microsoft

### Steps for each module

#### Client and Server 

cd ClientServer
docker-compose up --build 

The client UI is accessible at : http://localhost:5080/




### Notes 

I'm not sure the .vscode folder is needed to run this project locally. I don't think so, but I don't know this stack very well. I choose to ignore that folder in git. 

For reference  :

```
{
    "version": "0.2.0",
    "configurations": [
        {
            // Use IntelliSense to find out which attributes exist for C# debugging
            // Use hover for the description of the existing attributes
            // For further information visit https://github.com/OmniSharp/omnisharp-vscode/blob/master/debugger-launchjson.md
            "name": ".NET Core Launch (web)",
            "type": "coreclr",
            "request": "launch",
            "preLaunchTask": "build",
            // If you have changed target frameworks, make sure to update the program path.
            "program": "${workspaceFolder}/UI/BlazorApp/bin/Debug/net5.0/BlazorApp.dll",
            "args": [],
            "cwd": "${workspaceFolder}/UI/BlazorApp",
            "stopAtEntry": false,
            // Enable launching a web browser when ASP.NET Core starts. For more information: https://aka.ms/VSCode-CS-LaunchJson-WebBrowser
            "serverReadyAction": {
                "action": "openExternally",
                "pattern": "\\\\bNow listening on:\\\\s+(https?://\\\\S+)"
            },
            "env": {
                "ASPNETCORE_ENVIRONMENT": "Development"
            },
            "sourceFileMap": {
                "/Views": "${workspaceFolder}/Views"
            }
        },
        {
            "name": ".NET Core Attach",
            "type": "coreclr",
            "request": "attach",
            "processId": "${command:pickProcess}"
        }
    ]
}
````

````
{
    "version": "2.0.0",
    "tasks": [
        {
            "label": "build",
            "command": "dotnet",
            "type": "process",
            "args": [
                "build",
                "${workspaceFolder}/UI/BlazorApp/BlazorApp.csproj",
                "/property:GenerateFullPaths=true",
                "/consoleloggerparameters:NoSummary"
            ],
            "problemMatcher": "$msCompile"
        },
        {
            "label": "publish",
            "command": "dotnet",
            "type": "process",
            "args": [
                "publish",
                "${workspaceFolder}/UI/BlazorApp/BlazorApp.csproj",
                "/property:GenerateFullPaths=true",
                "/consoleloggerparameters:NoSummary"
            ],
            "problemMatcher": "$msCompile"
        },
        {
            "label": "watch",
            "command": "dotnet",
            "type": "process",
            "args": [
                "watch",
                "run",
                "${workspaceFolder}/UI/BlazorApp/BlazorApp.csproj",
                "/property:GenerateFullPaths=true",
                "/consoleloggerparameters:NoSummary"
            ],
            "problemMatcher": "$msCompile"
        }
    ]
}
````