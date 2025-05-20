using System.ComponentModel.DataAnnotations.Schema;

namespace RankingServer.Models;

[Table("userRank")]
public class UserRank
{
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public int UserScore { get; set; }
}