using CSharpFunctionalExtensions;

namespace Amai.SharedKernel.ValueObjects;

public class Path
{
    private Path(string value)
    {
        Value = value;
    }
    
    public string Value { get; private set; }

    public static Result<Path, Error> Create(
        string value)
    {
        return new Path(value);
    }
}