using MountAnything;

namespace MountConsul.Catalog;

public class NodeServiceHandler : PathHandler
{
    private readonly ConsulClient _client;

    public NodeServiceHandler(ItemPath path, IPathHandlerContext context, ConsulClient client) : base(path, context)
    {
        _client = client;
    }

    protected override IItem? GetItemImpl()
    {
        var nodeServices = _client.GetNodeServices(ItemName);
        var nodeService = nodeServices?.Services?.FirstOrDefault(s => s.Service == ItemName);

        return nodeService != null ? new NodeServiceItem(ParentPath, nodeService, nodeServices.Node.Address) : null;
    }

    protected override IEnumerable<IItem> GetChildItemsImpl()
    {
        yield break;
    }
}