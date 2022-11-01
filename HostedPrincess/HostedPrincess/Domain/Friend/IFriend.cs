using HostedPrincess.Domain.Hall;

namespace HostedPrincess.Domain.Friend;

public interface IFriend
{
    public Contender? CompareContenders(Contender contender1, Contender contender2);
}