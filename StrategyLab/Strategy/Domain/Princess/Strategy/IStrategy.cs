using Strategy.Domain.Hall;

namespace Strategy.Domain.Princess.Strategy;

public interface IStrategy
{
    public bool TestApplicant(Applicant applicant);
}