using System.Management.Automation;
using MountAnything;

namespace MountConsul;

public class ConsulDriveHandler : IDriveHandler, INewDriveParameters<ConsulDriveParameters>
{
    public PSDriveInfo NewDrive(PSDriveInfo driveInfo)
    {
        var config = new ConsulConfig
        {
            ConsulEndpoint = GetEndpoint(),
            AclToken = NewDriveParameters.AclToken
        };

        return new ConsulDriveInfo(config, driveInfo);
    }

    private Uri GetEndpoint()
    {
        if (NewDriveParameters.ConsulAddress.StartsWith("http://") || NewDriveParameters.ConsulAddress.StartsWith("https://"))
        {
            return new Uri(NewDriveParameters.ConsulAddress);
        }

        return new Uri($"http://{NewDriveParameters.ConsulAddress}");
    }

    public ConsulDriveParameters NewDriveParameters { get; set; }
}

public class ConsulDriveInfo : PSDriveInfo
{
    public ConsulDriveInfo(ConsulConfig consulConfig, PSDriveInfo driveInfo) : base(driveInfo)
    {
        ConsulConfig = consulConfig;
    }

    public ConsulConfig ConsulConfig { get; }
}