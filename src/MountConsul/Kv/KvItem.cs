using System.Management.Automation;
using Microsoft.PowerShell.Commands;
using MountAnything;

namespace MountConsul.Kv;

public class KvItem : Item
{
    private readonly IPathHandlerContext? _context;
    private readonly Lazy<PSObject>? _value;

    public KvItem(ItemPath parentPath, ItemPath directoryPath) : base(parentPath, new PSObject())
    {
        IsContainer = true;
        ItemName = directoryPath.Name;
        Key = directoryPath.FullName;
    }

    public KvItem(ItemPath parentPath, KeyMetadata key, IPathHandlerContext context) : base(parentPath, new PSObject(key))
    {
        _context = context;
        var keyPath = new ItemPath(key.Key);
        IsContainer = false;
        ItemName = keyPath.Name;
        Key = keyPath.FullName;
        _value = new Lazy<PSObject>(() => GetValue(key.RawValue));
    }

    private PSObject GetValue(string? rawValue)
    {
        if (string.IsNullOrEmpty(rawValue))
        {
            return new PSObject(rawValue);
        }

        if (LooksLikeJson(rawValue))
        {
            var cmd = new ConvertFromJsonCommand
            {
                InputObject = rawValue
            };

            try
            {
                return cmd.Invoke<PSObject>().Single();
            }
            catch (Exception ex)
            {
                _context?.WriteDebug(ex.ToString());
            }
        }

        return new PSObject(rawValue);
    }

    private bool LooksLikeJson(string rawValue)
    {
        return (rawValue.StartsWith("[") && rawValue.EndsWith("]") ||
                (rawValue.StartsWith("{") && rawValue.EndsWith("}")));
    }

    public override string ItemName { get; }
    public override bool IsContainer { get; }
    
    [ItemProperty]
    public string Key { get; }

    public string? Base64Value => Property<string>(nameof(KeyMetadata.Base64Value));

    [ItemProperty]
    public PSObject? Value => _value?.Value;
    public override string ItemType => IsContainer ? "Directory" : "Key";
}