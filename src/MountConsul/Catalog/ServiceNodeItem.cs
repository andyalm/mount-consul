using MountAnything;

namespace MountConsul.Catalog;

public class ServiceNodeItem : Item<ServiceNode>
{
    public ServiceNodeItem(ItemPath parentPath, ServiceNode serviceNode) : base(parentPath, serviceNode)
    {
        AddressAndPort = $"{UnderlyingObject.ServiceAddress}:{UnderlyingObject.ServicePort}";
        ItemName = AddressAndPort;
    }

    public override string ItemName { get; }
    public override bool IsContainer => false;

    [ItemProperty]
    public short Port => UnderlyingObject.ServicePort;

    [ItemProperty]
    public string AddressAndPort { get; }

    [ItemProperty]
    public string Name => UnderlyingObject.Name;

    [ItemProperty]
    public string[] Tags => UnderlyingObject.ServiceTags;
}