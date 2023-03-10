using System.Runtime.Versioning;
using MountAnything;

namespace MountConsul.Catalog;

public class CatalogHandler : PathHandler
{
    public static string LiteralItemName => "catalog";
    
    public static IItem CreateItem(ItemPath parentPath)
    {
        return new GenericContainerItem(parentPath, LiteralItemName)
        {
            Description = "Browse services and nodes in the consul catalog"
        };
    }
    
    public CatalogHandler(ItemPath path, IPathHandlerContext context) : base(path, context)
    {
    }

    protected override IItem GetItemImpl()
    {
        return CreateItem(ParentPath);
    }

    protected override IEnumerable<IItem> GetChildItemsImpl()
    {
        yield return NodesHandler.CreateItem(Path);
        yield return ServicesHandler.CreateItem(Path);
    }
}