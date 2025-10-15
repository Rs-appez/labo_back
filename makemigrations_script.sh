#!/bin/bash
dotnet ef migrations add "$1" -p src/Infrastructure -s src/Api
