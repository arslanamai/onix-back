using System.Data;

namespace Amai.Core.Abstraction;

public interface ISqlConnectionFactory
{
    IDbConnection Create();
}