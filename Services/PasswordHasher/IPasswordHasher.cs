namespace Services.PasswordHasher
{
    public interface IPasswordHasher
    {
        string Hash(string password, string salt);
        string GenerateSalt();
    }
}
