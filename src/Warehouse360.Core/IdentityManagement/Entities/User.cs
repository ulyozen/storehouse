using Warehouse360.Core.IdentityManagement.ValueObjects;
using Warehouse360.Core.SeedWork.Entities;

namespace Warehouse360.Core.IdentityManagement.Entities;

public class User : BaseEntity
{
    public string Username { get; private set; }
    public Email Email { get; private set; }
    public string PasswordHash { get; private set; }
    public List<Role> Roles { get; private set; }

    public User() 
    {
        Roles = new List<Role>();
    }
    
    public User(Guid id, string username, Email email, string passwordHash)
    {
        Id = id;
        Username = username;
        Email = email;
        PasswordHash = passwordHash;
        Roles = new List<Role>();
    }

    public void InitialRole(List<Role> roles)
    {
        Roles = roles;
    }

    public void AssignRole(Role role)
    {
        if (!Roles.Contains(role))
        {
            Roles.Add(role);
        }
    }

    public void RemoveRole(Role role)
    {
        if (Roles.Contains(role))
        {
            Roles.Remove(role);
        }
    }

    public void ChangePassword(string newPasswordHash)
    {
        PasswordHash = newPasswordHash ?? throw new ArgumentNullException(nameof(newPasswordHash));
    }
}