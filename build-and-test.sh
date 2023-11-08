#!/bin/bash
#lizard src/ -x"*/obj/*" -T cyclomatic_complexity=7 -w -W whitelizard.txt -l=csharp && \
dotnet build && \
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
