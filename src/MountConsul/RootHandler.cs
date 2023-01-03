using System.Runtime.Versioning;
using MountAnything;
using MountConsul.Catalog;
using MountConsul.Kv;

namespace MountConsul;

[RequiresPreviewFeatures]
public class RootHandler : PathHandler
{
    public RootHandler(ItemPath path, IPathHandlerContext context) : base(path, context)
    {
    }

    protected override IItem? GetItemImpl()
    {
        return new RootItem();
    }

    protected override IEnumerable<IItem> GetChildItemsImpl()
    {
        yield return CatalogHandler.CreateItem(Path);
        yield return KvHandler.CreateItem(Path);
    }
}