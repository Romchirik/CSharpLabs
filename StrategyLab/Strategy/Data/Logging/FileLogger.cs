using Strategy.Domain;
using Strategy.Domain.Hall;

namespace Strategy.Data.Logging;

public class FileLogger : IStrategyLogger
{
    private StreamWriter _writer;

    public FileLogger(string filePath)
    {
        _writer = new(filePath);
    }

    public void LogApplicant(Applicant applicant)
    {
        _writer.WriteLine($"{applicant.Surname} {applicant.Name}");
    }

    public void LogRating(int rating)
    {
        _writer.WriteLine(rating);
    }

    public void Dispose()
    {
        _writer.Dispose();
    }
}