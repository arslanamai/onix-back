using CSharpFunctionalExtensions;
using Onix.SharedKernel;
using Onix.SharedKernel.ValueObjects;
using Onix.SharedKernel.ValueObjects.Ids;
using Onix.WebSites.Domain.Locations.ValueObjects;
using DayOfWeek = Onix.WebSites.Domain.Locations.ValueObjects.DayOfWeek;

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
        Phone locationPhone,
        Address locationAddress) : base(id)
    {
        Name = name;
        LocationPhone = locationPhone;
        LocationAddress = locationAddress;
    }
    
    public Name Name { get; private set; }
    public Phone LocationPhone { get; private set; }
    public Address LocationAddress { get; private set; }

    public IReadOnlyList<Schedule> Schedules => _schedules;
    private readonly List<Schedule> _schedules = [];
    
    public static Result<Location> Create(
        LocationId id,
        Name name,
        Phone phone,
        Address locationAddress)
    {
        return new Location(
            id,
            name,
            phone,
            locationAddress);
    }

    public UnitResult<Error> Update(
        Name name,
        Phone phone,
        Address locationAddress)
    {
        this.Name = name;
        this.LocationPhone = phone;
        this.LocationAddress = locationAddress;
        
        return UnitResult.Success<Error>();
    }
    
    //исправить это
    public UnitResult<Error> AddSchedule(
        List<Schedule> schedules)
    {
        if (schedules.Count > Constants.SHARE_MAX_LENGTH)
            return UnitResult.Failure<Error>(
                Errors.Domain.MaxCount(ConstType.Schedule));
        
        _schedules.Clear();
        _schedules.AddRange(schedules);
        return UnitResult.Success<Error>();
    }
}