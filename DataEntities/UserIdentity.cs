#nullable disable

namespace DataEntities
{
    public class UserIdentity
    {
        public Guid UserId { get; set; }
        public string Salt { get; set; }

        public virtual User User { get; set; }
    }
}