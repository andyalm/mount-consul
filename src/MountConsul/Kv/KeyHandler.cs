using MountAnything;

namespace MountConsul.Kv;

public class KeyHandler : PathHandler
{
    private readonly KeyPath _keyPath;
    private readonly KvItemNavigator _navigator;
    private readonly ConsulClient _client;

    public KeyHandler(ItemPath path, IPathHandlerContext context, KeyPath keyPath, KvItemNavigator navigator, ConsulClient client) : base(path, context)
    {
        _keyPath = keyPath;
        _navigator = navigator;
        _client = client;
    }

    protected override IItem? GetItemImpl()
    {
        var key = _client.GetKeysRecursive(_keyPath.Path)
            .SingleOrDefault(k => k.Key == _keyPath.Path.FullName);

        return key != null ? new KvItem(ParentPath, key, Context) : null;
    }

    protected override IEnumerable<IItem> GetChildItemsImpl()
    {
        return _navigator.ListChildItems(Path, _keyPath.Path);
    }
}