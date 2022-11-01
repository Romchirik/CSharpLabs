namespace Strategy.Domain.Hall;

public interface IHallService
{
    public bool IsEmpty();

    public Applicant GetNextApplicant();
}