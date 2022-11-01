using Strategy.Domain.Hall;
using Strategy.Domain.Princess.Strategy;

namespace Strategy.Domain.Princess;

public class Princess
{
    private readonly IHallService _hall;
    private readonly IStrategy _strategy;
    private readonly IStrategyLogger _logger;

    public Princess(IHallService hall, IStrategy strategy, IStrategyLogger logger)
    {
        _hall = hall;
        _strategy = strategy;
        _logger = logger;
    }

    public Applicant? StartDating()
    {
        Applicant? candidate = null;
        while (true)
        {
            if (_hall.IsEmpty()) break;

            var nextApplicant = _hall.GetNextApplicant();
            _logger.LogApplicant(nextApplicant);
            if (_strategy.TestApplicant(nextApplicant))
            {
                candidate = nextApplicant;
                break;
            }
        }

        return candidate;
    }
}