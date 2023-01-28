using System.Text.Json.Serialization;

namespace MountConsul.Catalog;

public record ServiceNode
{
    public string ID { get; set; } = null!;

    [JsonPropertyName("Node")]
    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Datacenter { get; set; } = null!;
    
    public Dictionary<string, string> NodeMeta { get; set; } = new();

    public string ServiceAddress { get; set; } = null!;

    public ushort ServicePort { get; set; }

    public string[] ServiceTags { get; set; } = Array.Empty<string>();

    public Dictionary<string, string> ServiceMeta { get; set; } = new();
};