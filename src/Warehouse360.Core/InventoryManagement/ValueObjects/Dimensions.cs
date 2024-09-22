using Warehouse360.Core.SeedWork.ValueObjects;

namespace Warehouse360.Core.InventoryManagement.ValueObjects;

public class Dimensions : ValueObject
{
    public decimal Height { get; }
    public decimal Width { get; }
    public decimal Length { get; }

    public Dimensions(decimal height, decimal width, decimal length)
    {
        if (height <= 0 || width <= 0 || length <= 0)
            throw new ArgumentException("Dimensions must be positive values");

        Height = height;
        Width = width;
        Length = length;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}