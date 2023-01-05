using MountAnything;

namespace MountConsul.Catalog;

public class NodeItem : Item<Node>
{
    public NodeItem(ItemPath parentPath, Node node) : base(parentPath, node)
    {
        ItemName = node.Name;
    }

    public override string ItemName { get; }
    public override bool IsContainer => true;
}