using Warehouse360.Core.SeedWork.Entities;

namespace Warehouse360.Core.IdentityManagement.Entities;

public class Role : BaseEntity
{
    public string Name { get; private set; }
    public List<Permission> Permissions { get; private set; }

    public Role()
    {
        Permissions = new List<Permission>();
    }
    
    public Role(string name)
    {
        Name = name;
        Permissions = new List<Permission>();
    }

    public void AssignPermission(Permission permission)
    {
        if (!Permissions.Contains(permission))
        {
            Permissions.Add(permission);
        }
    }

    public void RemovePermission(Permission permission)
    {
        if (Permissions.Contains(permission))
        {
            Permissions.Remove(permission);
        }
    }
}