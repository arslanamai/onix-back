using CSharpFunctionalExtensions;

namespace Amai.SharedKernel.ValueObjects;

public record SubDomain
{
    private SubDomain(string value)
    {
        Value = value;
    }
    
    public string Value { get; }

    public static Result<SubDomain> Create(string value)
    {
        return new SubDomain(value);
    }
}