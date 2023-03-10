using System.Management.Automation;
using System.Runtime.Versioning;
using Microsoft.Extensions.DependencyInjection;
using MountAnything;
using MountAnything.Routing;
using MountConsul.Catalog;
using MountConsul.Kv;

namespace MountConsul;

public class MountConsulProvider : MountAnythingProvider<ConsulDriveParameters>
{
    public override Router CreateRouter()
    {
        var root = Router.Create<RootHandler>();
        root.ConfigureServices(ConfigureServices);
        
        root.Map<DatacenterHandler,Datacenter>(datacenter =>
        {
            datacenter.MapLiteral<CatalogHandler>(CatalogHandler.LiteralItemName, catalog =>
            {
                catalog.MapLiteral<NodesHandler>(NodesHandler.LiteralItemName, nodes =>
                {
                    nodes.Map<NodeHandler>(node =>
                    {
                        node.Map<NodeServiceHandler>();
                    });
                });
                catalog.MapLiteral<ServicesHandler>(ServicesHandler.LiteralItemName, services =>
                {
                    services.Map<ServiceHandler>(service =>
                    {
                        service.Map<ServiceNodeHandler>();
                    });
                });
            });
        
            datacenter.MapLiteral<KvHandler>(KvHandler.LiteralItemName, kv =>
            {
                kv.MapRecursive<KeyHandler,KeyPath>();
            });
        });

        return root;
    }

    protected override PSDriveInfo NewDrive(PSDriveInfo driveInfo, ConsulDriveParameters parameters)
    {
        var config = new ConsulConfig
        {
            ConsulEndpoint = parameters.GetEndpoint(),
            AclToken = parameters.AclToken
        };

        return new ConsulDriveInfo(config, driveInfo);
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient(s => GetConsulConfig(s.GetRequiredService<IPathHandlerContext>()));
        services.AddTransient<ConsulClient>();
        
        //will be overridden once in the datacenter directory
        services.AddTransient(_ => new Datacenter(""));
        
    }

    private ConsulConfig GetConsulConfig(IPathHandlerContext context)
    {
        var driveInfo = (ConsulDriveInfo)context.DriveInfo;

        return driveInfo.ConsulConfig;
    }
}
