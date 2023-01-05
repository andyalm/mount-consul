#!/usr/bin/env pwsh -NoExit -Interactive -NoLogo -NoProfile

param(
    [Parameter(Position=0,Mandatory=$true)]
    [string]
    $ConsulAddress,
    
    [Parameter(Position=1,Mandatory=$false)]
    [string]
    $AclToken,

    [Parameter()]
    [string]
    $Root = ''
)
$ErrorActionPreference='Stop'
$env:NO_MOUNT_CONSUL='1'
dotnet build
if(-not (Get-Alias ls -ErrorAction SilentlyContinue)) {
    New-Alias ls Get-ChildItem
}
if(-not (Get-Alias cat -ErrorAction SilentlyContinue)) {
    New-Alias cat Get-Content
}
Import-Module $([IO.Path]::Combine($PWD,'src','MountConsul','bin','Debug','net6.0','Module','MountConsul.psd1'))

New-PSDrive -Name consul -PSProvider MountConsul -Root $Root -ConsulAddress $ConsulAddress -AclToken $AclToken
cd consul:
