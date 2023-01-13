using MountAnything;

namespace MountConsul.Kv;

public class KvItemNavigator : ItemNavigator<KeyMetadata,KvItem>
{
    private readonly ConsulClient _client;
    private readonly IPathHandlerContext _context;

    public KvItemNavigator(ConsulClient client, IPathHandlerContext context)
    {
        _client = client;
        _context = context;
    }

    protected override KvItem CreateDirectoryItem(ItemPath parentPath, ItemPath directoryPath)
    {
        return new KvItem(parentPath, directoryPath);
    }

    protected override KvItem CreateItem(ItemPath parentPath, KeyMetadata model)
    {
        return new KvItem(parentPath, model, _context);
    }

    protected override ItemPath GetPath(KeyMetadata model)
    {
        return new ItemPath(model.Key);
    }

    protected override IEnumerable<KeyMetadata> ListItems(ItemPath? pathPrefix)
    {
        // keys with null value are directories, we don't want to return those
        return _client.GetKeysRecursive(pathPrefix).Where(k => k.Base64Value != null);
    }
}