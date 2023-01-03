using System.Text;
using System.Text.Json.Serialization;

namespace MountConsul.Kv;

public record KeyMetadata
{
    private readonly Lazy<string> _value;

    public KeyMetadata()
    {
        _value = new Lazy<string>(() => Encoding.UTF8.GetString(Convert.FromBase64String(Base64Value)));
    }

    public string Key { get; set; } = null!;
    
    [JsonPropertyName("Value")]
    public string Base64Value { get; set; } = null!;
    
    public string RawValue => _value.Value;
};