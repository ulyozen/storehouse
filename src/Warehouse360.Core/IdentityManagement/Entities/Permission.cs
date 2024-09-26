using Warehouse360.Core.SeedWork.Entities;

namespace Warehouse360.Core.IdentityManagement.Entities;

public class Permission : BaseEntity
{
    public string Name { get; private set; }

    public Permission(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
}