using System.Runtime.Versioning;
using MountAnything;

namespace MountConsul.Catalog;

public class ServicesHandler : PathHandler
{
    private readonly ConsulClient _client;

    public ServicesHandler(ItemPath path, IPathHandlerContext context, ConsulClient client) : base(path, context)
    {
        _client = client;
    }

    protected override IItem GetItemImpl()
    {
        return CreateItem(ParentPath);
    }

    protected override IEnumerable<IItem> GetChildItemsImpl()
    {
        return _client.GetServices().Select(s => new ServiceItem(Path, s));
    }

    public static string LiteralItemName => "services";
    public static IItem CreateItem(ItemPath parentPath)
    {
        return new GenericContainerItem(parentPath, LiteralItemName)
        {
            Description = "Browse consul services in the catalog"
        };
    }
}