using MountAnything;

namespace MountConsul.Catalog;

public class NodeServiceItem : Item<NodeService>
{
    public NodeServiceItem(ItemPath parentPath, NodeService service, string address) : base(parentPath, service)
    {
        ItemName = service.Service;
        Address = address;
    }

    [ItemProperty]
    public string Address { get; }

    [ItemProperty]
    public string AddressAndPort => $"{Address}:{UnderlyingObject.Port}";
    
    public override string ItemName { get; }
    public override bool IsContainer => false;
}