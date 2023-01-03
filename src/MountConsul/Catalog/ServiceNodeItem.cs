using MountAnything;

namespace MountConsul.Catalog;

public class ServiceNodeItem : Item<ServiceNode>
{
    public ServiceNodeItem(ItemPath parentPath, ServiceNode serviceNode) : base(parentPath, serviceNode)
    {
        ItemName = serviceNode.Name;
    }

    public override string ItemName { get; }
    public override bool IsContainer => false;
}