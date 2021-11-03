#nullable disable

namespace DataEntities
{
    public class User
    {
        public Guid Id { get; set; }
        public int RoleId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool EmailConfirmation { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual UserRole UserRole { get; set; }
    }
}
