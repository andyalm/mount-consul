using System.Management.Automation;
using MountAnything;

namespace MountConsul;

public class GenericContainerItem : Item
{
    public GenericContainerItem(ItemPath parentPath, string itemName) : base(parentPath, new PSObject())
    {
        ItemName = itemName;
    }
    
    [ItemProperty]
    public string? Description { get; init; }

    public override string ItemName { get; }
    public override bool IsContainer => true;
}