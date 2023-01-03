namespace MountConsul.Catalog;

public class Service
{
    public string Name { get; init; } = null!;
    public string[] Tags { get; init; } = Array.Empty<string>();
}