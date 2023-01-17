using DatabasePrincess.Domain.ContenderGenerator;
using DatabasePrincess.Domain.Friend;
using DatabasePrincess.Domain.Hall;

namespace DatabasePrincess.Data.Friend;

public class FriendImpl : IFriend
{
    private readonly IHall _hall;
    private readonly Dictionary<Contender, int> _contenders;

    public FriendImpl(IContenderGenerator generator, IHall hall)
    {
        _hall = hall;
        _contenders = new Dictionary<Contender, int>(generator.CreateApplicantsWithRating());
    }

    public Contender? CompareContenders(Contender contender1, Contender contender2)
    {
        if (!_hall.IsContenderEmitted(contender1) || !_hall.IsContenderEmitted(contender2))
        {
            throw new InvalidOperationException("Illegal contender");
        }

        if (_contenders[contender1] > _contenders[contender2])
        {
            return contender1;
        }

        if (_contenders[contender1] < _contenders[contender2])
        {
            return contender2;
        }

        return null;
    }
}