namespace Warehouse360.Core.SeedWork.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; protected set; }

    // Общая логика для всех сущностей
    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is BaseEntity))
            return false;

        return Id == ((BaseEntity)obj).Id;
    }

    public override int GetHashCode() => Id.GetHashCode();
}