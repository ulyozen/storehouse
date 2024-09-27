using Warehouse360.Core.SeedWork.Entities;

namespace Warehouse360.Core.IdentityManagement.Entities;

public class Permission : BaseEntity
{
    public string Name { get; private set; }

    public Permission() { }
    
    public Permission(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}