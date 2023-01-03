# Mount Consul

An experimental powershell provider that allows you to explore consul as a virtual filesystem.

## Installation

```powershell
Install-Module MountConsul

# Consider adding the following lines to your profile:
Import-Module MountConsul

# Add a PSDrive for every instance of consul you wish to mount. Add this to your profile so that it will always be present
New-PSDrive -Name consul -PSProvider MountConsul -Root '' -ConsulAddress 127.0.0.1:8500 -AclToken 'optional acl token'
```

## Usage

Once you have mounted a PSDrive for your consul instance, you can navigate it.
You can run `dir` within any directory to list the objects within. This essentially provides a self-documenting
way of navigating. Tab completion works as well.

The virtual filesystem is constructed like this (values surrounded by <> are dynamic):

```
 -- <datacenter>
    |-- catalog
        |-- services
            |-- <service-name>
                |-- <service-node>
    |-- kv
        |-- <key>
            |-- <key>
                ...
```
