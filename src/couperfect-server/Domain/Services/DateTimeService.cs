namespace CouperfectServer.Domain.Services;

public interface IDateTimeService
{
    DateTime Now { get; }
}

public class DateTimeService : IDateTimeService
{
    public DateTime Now => DateTime.UtcNow;
}

