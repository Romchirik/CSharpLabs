using DatabasePrincess.Domain.Hall;

namespace DatabasePrincess.Domain.Friend;

public interface IFriend
{
    public Contender? CompareContenders(Contender contender1, Contender contender2);
}