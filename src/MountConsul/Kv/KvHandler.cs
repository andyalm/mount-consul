using System.Runtime.Versioning;
using MountAnything;

namespace MountConsul.Kv;

[RequiresPreviewFeatures]
public class KvHandler : PathHandler, ILiteralPathHandler
{
    private readonly KvItemNavigator _navigator;

    public static IItem CreateItem(ItemPath parentPath)
    {
        return new GenericContainerItem(parentPath, LiteralItemName)
        {
            Description = "Browse consul's key value store"
        };
    }

    public static string LiteralItemName => "kv";
    
    public KvHandler(ItemPath path, IPathHandlerContext context, KvItemNavigator navigator) : base(path, context)
    {
        _navigator = navigator;
    }

    protected override IItem GetItemImpl()
    {
        return CreateItem(ParentPath);
    }

    protected override IEnumerable<IItem> GetChildItemsImpl()
    {
        return _navigator.ListChildItems(Path);
    }
}