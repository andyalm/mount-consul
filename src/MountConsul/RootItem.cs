using System.Management.Automation;
using MountAnything;

namespace MountConsul;

public class RootItem : Item
{
    public RootItem() : base(ItemPath.Root, new PSObject())
    {
        
    }
    
    public override string ItemName => string.Empty;

    public override bool IsContainer => true;
}