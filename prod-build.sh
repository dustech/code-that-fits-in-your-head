#!/bin/bash
dotnet clean && \
dotnet build --configuration Release && \
dotnet test --configuration Release
