namespace Warehouse360.Application.IdentityManagement.Dtos;

public class PermissionDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public PermissionDto(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}