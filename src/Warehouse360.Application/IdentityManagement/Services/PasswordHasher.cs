using System.Security.Cryptography;
using System.Text;
using Warehouse360.Core.IdentityManagement.Services;

namespace Warehouse360.Application.IdentityManagement.Services;

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string hashedPassword, string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}