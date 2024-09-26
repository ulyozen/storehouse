namespace Warehouse360.Core.IdentityManagement.Services;

public interface IPasswordHasher
{
    string HashPassword(string password);
    bool VerifyPassword(string hashedPassword, string providedPassword);
}