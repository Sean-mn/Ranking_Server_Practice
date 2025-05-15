namespace RankingServer.Requests;

[Serializable]
public class UserSignUpRequest
{
    public string Username { get; set; } 
    public string Password { get; set; }
}