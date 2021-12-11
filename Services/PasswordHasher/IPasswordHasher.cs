﻿namespace MyTutoring.Services.PasswordHasher
{
    public interface IPasswordHasher
    {
        string GenerateSalt();
        string Hash(string password, string salt);
    }
}
