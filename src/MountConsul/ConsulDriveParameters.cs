using System.Management.Automation;

namespace MountConsul;

public class ConsulDriveParameters
{
    [Parameter(Mandatory = true)]
    public string ConsulAddress { get; set; } = null!;

    [Parameter]
    public string? AclToken { get; set; }

    public Uri GetEndpoint()
    {
        if (ConsulAddress.StartsWith("http://") || ConsulAddress.StartsWith("https://"))
        {
            return new Uri(ConsulAddress);
        }

        return new Uri($"http://{ConsulAddress}");
    }
}