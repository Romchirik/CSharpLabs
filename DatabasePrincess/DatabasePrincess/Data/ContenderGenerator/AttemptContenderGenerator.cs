using DatabasePrincess.Domain.Attempts;
using DatabasePrincess.Domain.ContenderGenerator;
using DatabasePrincess.Domain.Hall;
using DatabasePrincess.Domain.Model;

namespace DatabasePrincess.Data.ContenderGenerator;

public class AttemptContenderGenerator : IContenderGenerator
{
    private readonly int _attemptId;
    private readonly IAttemptsRepo _attemptsRepo;

    public AttemptContenderGenerator(AttemptId attemptId, IAttemptsRepo attemptsRepo)
    {
        _attemptId = attemptId.Id;
        _attemptsRepo = attemptsRepo;
    }

    public List<KeyValuePair<Contender, int>> CreateApplicantsWithRating()
    {
        var attempt = _attemptsRepo.GetAttempt(_attemptId);
        return attempt.Contenders.ConvertAll(savedContender =>
            new KeyValuePair<Contender, int>(
                new Contender(savedContender.Id, savedContender.Surname, savedContender.Name),
                savedContender.rating));
    }
}