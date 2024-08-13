
namespace Shared.Lib.Entities
{
    public class AppRefreshToken
    {
        public int Id { get; set; }
        public string? Token { get; set; }
        public int UserId { get; set; }
    }
}
