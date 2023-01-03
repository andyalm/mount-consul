using System.Management.Automation;

namespace MountConsul;

public class ConsulDriveParameters
{
    [Parameter(Mandatory = true)]
    public string ConsulAddress { get; set; }

    [Parameter]
    public string? AclToken { get; set; }
}