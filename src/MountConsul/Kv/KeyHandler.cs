using MountAnything;
using MountAnything.Content;

namespace MountConsul.Kv;

public class KeyHandler : PathHandler, IContentReaderHandler
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
        var allKeys = _client.GetKeysRecursive(_keyPath.Path).ToArray();
            
        var matchingKey = allKeys.SingleOrDefault(k => k.Key == _keyPath.Path.FullName);
        if (matchingKey != null)
        {
            return new KvItem(ParentPath, matchingKey, Context);
        }

        var childKeys = allKeys.Where(k => k.Key.StartsWith(_keyPath.Path.FullName + ItemPath.Separator));
        if (childKeys.Any())
        {
            return new KvItem(ParentPath, _keyPath.Path);
        }

        return null;
    }

    protected override IEnumerable<IItem> GetChildItemsImpl()
    {
        return _navigator.ListChildItems(Path, _keyPath.Path);
    }
    
    public IStreamContentReader GetContentReader()
    {
        var item = GetItem() as KvItem;

        if (string.IsNullOrEmpty(item?.Base64Value))
        {
            return new EmptyContentReader();
        }

        return new StreamContentReader(new MemoryStream(Convert.FromBase64String(item.Base64Value)));
    }
}