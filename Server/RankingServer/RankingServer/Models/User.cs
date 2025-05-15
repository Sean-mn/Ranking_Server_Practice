using System.ComponentModel.DataAnnotations.Schema;

namespace RankingServer.Models;

[Table("user")]
public class User
{
    public int Id {get; set;}
    public string Username {get; set;}
    public string Password {get; set;}
}