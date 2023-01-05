using System.Runtime.Versioning;
using MountAnything;

namespace MountConsul.Catalog;

[RequiresPreviewFeatures]
public class NodesHandler : PathHandler, ILiteralPathHandler
{
    private readonly ConsulClient _client;

    public NodesHandler(ItemPath path, IPathHandlerContext context, ConsulClient client) : base(path, context)
    {
        _client = client;
    }

    protected override IItem? GetItemImpl()
    {
        return CreateItem(ParentPath);
    }

    protected override IEnumerable<IItem> GetChildItemsImpl()
    {
        return _client.GetNodes().Select(n => new NodeItem(Path, n));
    }

    public static string LiteralItemName => "nodes";
    
    public static IItem CreateItem(ItemPath parentPath)
    {
        return new GenericContainerItem(parentPath, LiteralItemName)
        {
            Description = "Browse nodes in the service catalog"
        };
    }
}