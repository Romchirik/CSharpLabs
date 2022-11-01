using HostedPrincess.Domain.Hall;

namespace HostedPrincess.Domain.ContenderGenerator;

public interface IContenderGenerator
{
    public List<KeyValuePair<Contender, int>> CreateApplicantsWithRating();
}