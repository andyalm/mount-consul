using MountAnything;

namespace MountConsul.Catalog;

public class NodeHandler : PathHandler
{
    private readonly ConsulClient _client;

    public NodeHandler(ItemPath path, IPathHandlerContext context, ConsulClient client) : base(path, context)
    {
        _client = client;
    }

    protected override IItem? GetItemImpl()
    {
        var node = _client.GetNode(ItemName);
        return node != null ? new NodeItem(ParentPath, node) : null;
    }

    protected override IEnumerable<IItem> GetChildItemsImpl()
    {
        var response = _client.GetNodeServices(ItemName);

        return response.Services.Select(s => new NodeServiceItem(Path, s, response.Node.Address));
    }
}