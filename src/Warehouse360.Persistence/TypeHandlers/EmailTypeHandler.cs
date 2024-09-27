using System.Data;
using Dapper;
using Warehouse360.Core.IdentityManagement.ValueObjects;

namespace Warehouse360.Persistence.TypeHandlers;

public class EmailTypeHandler : SqlMapper.TypeHandler<Email>
{
    public override void SetValue(IDbDataParameter parameter, Email? value)
    {
        parameter.Value = value!.Value;
    }
    
    public override Email? Parse(object value)
    {
        return new Email(value.ToString());
    }
}