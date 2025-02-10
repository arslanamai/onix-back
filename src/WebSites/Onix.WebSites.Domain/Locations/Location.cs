using CSharpFunctionalExtensions;
using Onix.SharedKernel;
using Onix.SharedKernel.ValueObjects;
using Onix.SharedKernel.ValueObjects.Ids;

namespace Onix.WebSites.Domain.Locations;

public class Location : SharedKernel.Entity<LocationId>
{
    //ef core
    private Location(LocationId id) : base(id)
    {
    }
    
    private Location(
        LocationId id,
        Name name,
        Code code) : base(id)
    {
        Name = name;
        Code = code;
    }
    
    public Name Name { get; private set; }
    public Code Code { get; private set; }
    
    
    public static Result<Location> Create(
        LocationId id,
        Name name,
        Code code)
    {
        return new Location(
            id,
            name,
            code);
    }

    public UnitResult<Error> Update(
        Name name,
        Code code)
    {
        this.Name = name;
        this.Code = code;
        
        return UnitResult.Success<Error>();
    }
}