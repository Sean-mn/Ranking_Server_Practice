namespace RankingServer.Sessions;

public abstract class Session
{
    public string SessionId { get; set; } = Guid.NewGuid().ToString();
    public DateTime Created { get; set; } = DateTime.UtcNow;

    public abstract string ToJson();
}