using Strategy.Domain.Hall;

namespace Strategy.Domain.Rating;

public interface IRatingService
{
    public int GetRating(Applicant applicant);
}