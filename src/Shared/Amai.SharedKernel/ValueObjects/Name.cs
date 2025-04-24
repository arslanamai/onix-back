using CSharpFunctionalExtensions;

namespace Amai.SharedKernel.ValueObjects;

public class Name
{
    private Name(string value)
    {
        Value = value;
    }
    
    public string Value { get; }

    public static Result<Name> Create(string value)
    {
        return new Name(value);
    }
}