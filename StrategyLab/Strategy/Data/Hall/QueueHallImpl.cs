using Strategy.Domain.Hall;

namespace Strategy.Data.Hall;

public class QueueHallImpl : IHallService
{
    private readonly Queue<Applicant> _applicants;

    public QueueHallImpl(IEnumerable<Applicant> applicants)
    {
        _applicants = new Queue<Applicant>(applicants.OrderBy(_ => Guid.NewGuid()));
    }

    public bool IsEmpty()
    {
        return _applicants.Count == 0;
    }

    public Applicant GetNextApplicant()
    {
        return _applicants.Dequeue();
    }
}