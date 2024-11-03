using CSharpFunctionalExtensions;

namespace Onix.WebSites.Domain.Locations.ValueObjects;

public record Schedule
{
    private Schedule(
        string weekDay,
        string startTime,
        string endTime)
    {
        WeekDay = weekDay;
        StartTime = startTime;
        EndTime = endTime;
    }
    
    public string WeekDay { get; }
    public string StartTime { get; }
    public string EndTime { get; }

    public static Result<List<Schedule>> Create(
        List<Schedule> schedules)
    {
        return new List<Schedule>(schedules);
    }
}