using Strategy.Domain.Hall;
using Strategy.Domain.Rating;

namespace Strategy.Data.Rating;

public class RatingServiceImpl : IRatingService
{
    private readonly Dictionary<Applicant, int> _ratingMap;

    public RatingServiceImpl(List<Applicant> applicants)
    {
        Random rnd = new Random();
        var ratingsTmp = Enumerable.Range(1, applicants.Count).ToList().OrderBy(a => rnd.Next());
        var pairs = applicants.Zip(ratingsTmp,
            (first, second) => new KeyValuePair<Applicant, int>(first, second));
        _ratingMap = new Dictionary<Applicant, int>(pairs);
    }


    public int GetRating(Applicant applicant)
    {
        return _ratingMap[applicant];
    }
}