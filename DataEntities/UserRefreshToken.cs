#nullable disable

namespace DataEntities
{
    public class UserRefreshToken
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }

        public virtual User User { get; set; }
    }
}
