using DatabasePrincess.Domain.Friend;
using DatabasePrincess.Domain.Hall;
using Microsoft.Extensions.Logging;

namespace DatabasePrincess.Domain.Princess;

public class Princess
{
    private readonly IHall _hall;
    private readonly IFriend _friend;
    private readonly ILogger<Princess> _logger;
    private const int PassContenders = 5;
    private readonly List<Contender> _anotherContenders = new();

    public Princess(
        IHall hall,
        IFriend friend,
        ILogger<Princess> logger
    )
    {
        _hall = hall;
        _friend = friend;
        _logger = logger;
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
                _logger.LogInformation("accepted: {0} {1}", selectedContender.Surname, selectedContender.Name);
                break;
            }

            _logger.LogInformation("rejected: {0} {1}", nextContender.Surname, nextContender.Name);
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