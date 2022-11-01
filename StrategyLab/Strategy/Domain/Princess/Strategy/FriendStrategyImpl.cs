using Strategy.Domain.Hall;
using Strategy.Domain.Rating;

namespace Strategy.Domain.Princess.Strategy;

public class FriendStrategyImpl : IStrategy
{
    private readonly IRatingService _ratingService;
    private readonly int _passApplicants;
    private readonly List<Applicant> _anotherApplicants = new();

    public FriendStrategyImpl(IRatingService ratingService, int passApplicants)
    {
        _ratingService = ratingService;
        _passApplicants = passApplicants;
    }


    public bool TestApplicant(Applicant applicant)
    {
        if (_anotherApplicants.Count > _passApplicants)
        {
            bool coolerThanOthers = true;
            foreach (var anotherApplicant in _anotherApplicants)
            {
                if (_ratingService.GetRating(anotherApplicant) > _ratingService.GetRating(applicant))
                {
                    coolerThanOthers = false;
                }
            }

            _anotherApplicants.Add(applicant);
            return coolerThanOthers;
        }

        _anotherApplicants.Add(applicant);
        return false;
    }
}