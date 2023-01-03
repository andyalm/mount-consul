using System.Text.Json.Serialization;

namespace MountConsul.Catalog;

public record ServiceNode
{
    public string ID { get; set; }
    
    [JsonPropertyName("Node")]
    public string Name { get; set; }

    public string Address { get; set; }

    public string Datacenter { get; set; }
};