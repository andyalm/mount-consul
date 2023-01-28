using MountAnything;
using MountConsul.Catalog;
using MountConsul.Kv;

namespace MountConsul;

public class DatacenterHandler : PathHandler
{
    private readonly ConsulClient _client;

    public DatacenterHandler(ItemPath path, IPathHandlerContext context, ConsulClient client) : base(path, context)
    {
        _client = client;
    }

    protected override IItem? GetItemImpl()
    {
        var dc = _client.ListDatacenters().FirstOrDefault(dc => dc == ItemName);

        return dc != null ? new DatacenterItem(ParentPath, dc) : null;
    }

    protected override IEnumerable<IItem> GetChildItemsImpl()
    {
        yield return CatalogHandler.CreateItem(Path);
        yield return KvHandler.CreateItem(Path);;
    }
}