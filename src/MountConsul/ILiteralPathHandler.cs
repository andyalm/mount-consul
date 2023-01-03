using System.Reflection;
using System.Runtime.Versioning;
using MountAnything;

namespace MountConsul;

[RequiresPreviewFeatures]
public interface ILiteralPathHandler : IPathHandler
{
    static abstract string LiteralItemName { get; }
    
    static abstract IItem CreateItem(ItemPath parentPath);
}
