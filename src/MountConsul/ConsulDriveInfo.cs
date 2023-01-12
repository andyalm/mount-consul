using System.Management.Automation;
using MountAnything;

namespace MountConsul;

public class ConsulDriveInfo : PSDriveInfo
{
    public ConsulDriveInfo(ConsulConfig consulConfig, PSDriveInfo driveInfo) : base(driveInfo)
    {
        ConsulConfig = consulConfig;
    }

    public ConsulConfig ConsulConfig { get; }
}