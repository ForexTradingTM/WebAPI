using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ForexTradingBotWebAPI.Domain.User;

public class User
{
    [Key]
    public string Email { get; set; }
    
    public DateTime RegistrationDate { get; set; }
    public DateTime ModifiedDate { get; set; }

    [JsonIgnore]
    public string? PasswordHash { get; set; }
}