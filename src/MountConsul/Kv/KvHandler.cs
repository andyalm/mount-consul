using System.Runtime.Versioning;
using MountAnything;

namespace MountConsul.Kv;

[RequiresPreviewFeatures]
public class KvHandler : PathHandler, ILiteralPathHandler
{
    public static IItem CreateItem(ItemPath parentPath)
    {
        return new GenericContainerItem(parentPath, LiteralItemName)
        {
            Description = "Browse consul's key value store"
        };
    }

    public static string LiteralItemName => "kv";
    
    public KvHandler(ItemPath path, IPathHandlerContext context) : base(path, context)
    {
    }

    protected override IItem? GetItemImpl()
    {
        return CreateItem(ParentPath);
    }

    protected override IEnumerable<IItem> GetChildItemsImpl()
    {
        yield break;
    }
}