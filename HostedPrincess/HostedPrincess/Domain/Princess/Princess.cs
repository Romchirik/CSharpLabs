using HostedPrincess.Domain.Friend;
using HostedPrincess.Domain.Hall;

namespace HostedPrincess.Domain.Princess;

public class Princess
{
    private readonly IHall _hall;
    private readonly IFriend _friend;
    private const int PassContenders = 5;
    private readonly List<Contender> _anotherContenders = new();

    public Princess(
        IHall hall,
        IFriend friend
    )
    {
        _hall = hall;
        _friend = friend;
    }
    
    public Contender? SelectContender()
    {
        Contender? selectedContender = null;
        while (!_hall.IsEmpty())
        {
            var nextContender = _hall.GetNextContender();

            if (TestApplicant(nextContender))
            {
                selectedContender = nextContender;
                break;
            }
        }

        return selectedContender;
    }

    private bool TestApplicant(Contender nextContender)
    {
        if (_anotherContenders.Count > PassContenders)
        {
            bool coolerThanOthers = true;
            foreach (var anotherContender in _anotherContenders)
            {
                if (_friend.CompareContenders(anotherContender, nextContender) != nextContender)
                {
                    coolerThanOthers = false;
                    break;
                }
            }

            _anotherContenders.Add(nextContender);
            return coolerThanOthers;
        }

        _anotherContenders.Add(nextContender);
        return false;
    }
}