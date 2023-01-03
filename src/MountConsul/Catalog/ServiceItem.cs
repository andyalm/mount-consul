using MountAnything;

namespace MountConsul.Catalog;

public class ServiceItem : Item<Service>
{
    public ServiceItem(ItemPath parentPath, Service service) : base(parentPath, service)
    {
        ItemName = service.Name;
    }

    public override string ItemName { get; }
    
    public override bool IsContainer => true;
}