
namespace Server.Business.Security.JWT;

public class TokenOptions
{
    public string? Key { get; set; }
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
}
