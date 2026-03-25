using System.Data;
using Dapper;

public class DateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly>
{
    // writes
     public override void SetValue(IDbDataParameter parameter, DateOnly value)
    {
        parameter.Value = value.ToString("yyyy-MM-dd");
    }
    //reads
    public override DateOnly Parse(object value)
    {
        if (value is string StringValue)
        {
            return DateOnly.Parse(StringValue);
        }
        return DateOnly.FromDateTime((DateTime)value);
    }
}