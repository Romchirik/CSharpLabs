using Strategy.Domain.Hall;

namespace Strategy.Domain;

public interface IStrategyLogger: IDisposable   
{
    public void LogApplicant(Applicant applicant);
    public void LogRating(int rating);
}