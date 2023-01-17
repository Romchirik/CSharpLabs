using DatabasePrincess.Domain.Hall;

namespace DatabasePrincess.Domain.ContenderGenerator;

public interface IContenderGenerator
{
    public List<KeyValuePair<Contender, int>> CreateApplicantsWithRating();
}