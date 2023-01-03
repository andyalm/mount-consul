using System.Management.Automation;
using MountAnything;

namespace MountConsul;

public class DatacenterItem : Item
{
    public DatacenterItem(ItemPath parentPath, string datacenterName) : base(parentPath, new PSObject())
    {
        ItemName = datacenterName;
    }

    public override string ItemName { get; }
    public override bool IsContainer => true;
}