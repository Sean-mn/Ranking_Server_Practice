using System.Text.Json;

namespace RankingServer.Sessions;

public class LoginSession : Session
{
    public int UserId { get; set; }
    public string Username { get; set; }

    public override string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }

    public static LoginSession FromJson(string json)
    {
        return JsonSerializer.Deserialize<LoginSession>(json);
    }
}