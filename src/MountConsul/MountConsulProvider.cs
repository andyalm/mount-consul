using System.Runtime.Versioning;
using Autofac;
using MountAnything;
using MountAnything.Routing;
using MountConsul.Catalog;
using MountConsul.Kv;

namespace MountConsul;

[RequiresPreviewFeatures]
public class MountConsulProvider : IMountAnythingProvider
{
    public Router CreateRouter()
    {
        var root = Router.Create<RootHandler>();
        root.RegisterServices(RegisterServices);
        
        root.Map<DatacenterHandler,Datacenter>(datacenter =>
        {
            datacenter.MapLiteral<CatalogHandler>(catalog =>
            {
                catalog.Map<ServicesHandler>(services =>
                {
                    services.Map<ServiceHandler>(service =>
                    {
                        service.Map<ServiceNodeHandler>();
                    });
                });
            });
        
            datacenter.MapLiteral<KvHandler>(kv =>
            {
                kv.MapRecursive<KeyHandler,KeyPath>();
            });
        });

        return root;
    }

    public IDriveHandler GetDriveHandler() => new ConsulDriveHandler();

    private void RegisterServices(ContainerBuilder builder)
    {
        builder.Register(c => GetConsulConfig(c.Resolve<IPathHandlerContext>()));
        builder.RegisterType<ConsulClient>();
        builder.Register(c => new Datacenter(""));
    }

    private ConsulConfig GetConsulConfig(IPathHandlerContext context)
    {
        var driveInfo = (ConsulDriveInfo)context.DriveInfo;

        return driveInfo.ConsulConfig;
    }
}
