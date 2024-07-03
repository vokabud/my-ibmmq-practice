namespace IbmmqConnect.Api.IbmMq;

public class Configuration
{
    public required string QueueManager { get; set; }

    public required string Channel { get; set; }

    public required string Host { get; set; }

    public int Port { get; set; }

    public string? UserId { get; set; }

    public string? Password { get; set; }
}
