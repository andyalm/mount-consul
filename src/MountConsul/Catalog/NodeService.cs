namespace MountConsul.Catalog;

public record NodeService
{
    public string ID { get; set; } = null!;
    public string Service { get; set; } = null!;
    public string[] Tags { get; set; } = Array.Empty<string>();
    public Dictionary<string, string> Meta { get; set; } = new();
    public ushort? Port { get; set; }
}