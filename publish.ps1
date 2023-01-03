$ErrorActionPreference='Stop'

dotnet publish MountConsul
Get-ChildItem -Recurse ./bin/MountConsul | Select-Object Name
Publish-Module -Path ./bin/MountConsul -NuGetApiKey $env:NuGetApiKey
