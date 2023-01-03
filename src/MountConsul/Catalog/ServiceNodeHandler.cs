using MountAnything;

namespace MountConsul.Catalog;

public class ServiceNodeHandler : PathHandler
{
    private readonly ConsulClient _client;

    public ServiceNodeHandler(ItemPath path, IPathHandlerContext context, ConsulClient client) : base(path, context)
    {
        _client = client;
    }

    protected override IItem? GetItemImpl()
    {
        var node = _client.GetServiceNode(ParentPath.Name, ItemName);

        return node != null ? new ServiceNodeItem(ParentPath, node) : null;
    }

    protected override IEnumerable<IItem> GetChildItemsImpl()
    {
        yield break;
    }
}