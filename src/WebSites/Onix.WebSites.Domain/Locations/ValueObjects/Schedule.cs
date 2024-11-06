using CSharpFunctionalExtensions;

namespace Onix.WebSites.Domain.Locations.ValueObjects;

public record Schedule
{
    public Schedule()
    {
    }
    
    private Schedule(
        string weekDay,
        string startTime,
        string endTime)
    {
        WeekDay = weekDay;
        StartTime = startTime;
        EndTime = endTime;
    }
    
    public string WeekDay { get; init; }
    public string StartTime { get; init; }
    public string EndTime { get; init; }

    public static Result<Schedule> Create(
        string weekDay,
        string startDay,
        string endTime)
    {
        return new Schedule(weekDay,startDay,endTime);
    }
}