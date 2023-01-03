using System.Management.Automation;

namespace MountConsul;

public class ConsulDriveParameters
{
    [Parameter(Mandatory = true)]
    public string ConsulAddress { get; set; } = null!;

    [Parameter]
    public string? AclToken { get; set; }
}