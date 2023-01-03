using MountAnything;

namespace MountConsul.Catalog;

public class ServiceHandler : PathHandler
{
    private readonly ConsulClient _client;

    public ServiceHandler(ItemPath path, IPathHandlerContext context, ConsulClient client) : base(path, context)
    {
        _client = client;
    }

    protected override IItem? GetItemImpl()
    {
        var service = _client.GetService(ItemName);

        return service != null ? new ServiceItem(ParentPath, service) : null;
    }

    protected override IEnumerable<IItem> GetChildItemsImpl()
    {
        return _client.GetServiceNodes(ItemName)
            .Select(n => new ServiceNodeItem(Path, n));
    }
}