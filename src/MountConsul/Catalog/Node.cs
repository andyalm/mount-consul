using System.Text.Json.Serialization;

namespace MountConsul.Catalog;

public record Node
{
    public string ID { get; set; } = null!;

    [JsonPropertyName("Node")]
    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Datacenter { get; set; } = null!;

    public Dictionary<string, string> Meta { get; set; } = new();
};