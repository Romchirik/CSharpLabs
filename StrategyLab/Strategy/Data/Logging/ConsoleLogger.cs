using Strategy.Domain;
using Strategy.Domain.Hall;

namespace Strategy.Data.Logging;

public class ConsoleLogger : IStrategyLogger
{
    public void LogApplicant(Applicant applicant)
    {
        Console.WriteLine($"{applicant.Surname} {applicant.Name}");
    }

    public void LogRating(int rating)
    {
        Console.WriteLine(rating);
    }

    public void Dispose()
    {
            
    }
}