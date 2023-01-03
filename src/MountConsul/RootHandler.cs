using System.Runtime.Versioning;
using MountAnything;
using MountConsul.Catalog;
using MountConsul.Kv;

namespace MountConsul;

[RequiresPreviewFeatures]
public class RootHandler : PathHandler
{
    private readonly ConsulClient _client;

    public RootHandler(ItemPath path, IPathHandlerContext context, ConsulClient client) : base(path, context)
    {
        _client = client;
    }

    protected override IItem GetItemImpl()
    {
        return new RootItem();
    }

    protected override IEnumerable<IItem> GetChildItemsImpl()
    {
        return _client.ListDatacenters().Select(dc => new DatacenterItem(Path, dc));
    }
}