namespace MountConsul.Catalog;

public record NodeServicesResponse
{
    public Node Node { get; set; } = null!;
    public NodeService[] Services { get; set; } = Array.Empty<NodeService>();
};