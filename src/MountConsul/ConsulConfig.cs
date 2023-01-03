namespace MountConsul;

public record ConsulConfig
{
    public Uri ConsulEndpoint { get; set; } = null!;

    public string? AclToken { get; set; }
}