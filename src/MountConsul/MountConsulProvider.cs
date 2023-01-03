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
        
        
        root.MapLiteral<CatalogHandler>(catalog =>
        {
            catalog.Map<ServicesHandler>(services =>
            {
                services.Map<ServiceHandler>(service =>
                {
                    service.Map<ServiceNodeHandler>();
                });
            });
        });
        
        root.MapLiteral<KvHandler>(kv =>
        {
            
        });

        return root;
    }

    public IDriveHandler GetDriveHandler() => new ConsulDriveHandler();

    private void RegisterServices(ContainerBuilder builder)
    {
        builder.Register(c => GetConsulConfig(c.Resolve<IPathHandlerContext>()));
        builder.RegisterType<ConsulClient>();
    }

    private ConsulConfig GetConsulConfig(IPathHandlerContext context)
    {
        var driveInfo = (ConsulDriveInfo)context.DriveInfo;

        return driveInfo.ConsulConfig;
    }
}
