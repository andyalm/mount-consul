$ErrorActionPreference='Stop'

dotnet publish src/MountConsul
Get-ChildItem -Recurse ./bin/MountConsul | Select-Object Name
Publish-Module -Path ./bin/MountConsul -NuGetApiKey $env:NuGetApiKey
