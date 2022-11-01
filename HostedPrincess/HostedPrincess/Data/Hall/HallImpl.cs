using HostedPrincess.Domain.ContenderGenerator;
using HostedPrincess.Domain.Hall;

namespace HostedPrincess.Data.Hall;

public class HallImpl : IHall
{
    private readonly Queue<KeyValuePair<Contender, int>> _waitingContenders;
    private readonly Dictionary<Contender, int> _emittedContenders = new();
    private bool _contenderSelected = false;

    public HallImpl(IContenderGenerator generator)
    {
        _waitingContenders = new Queue<KeyValuePair<Contender, int>>(
            generator.CreateApplicantsWithRating());
    }

    public Contender GetNextContender()
    {
        if (_contenderSelected)
        {
            throw new InvalidOperationException("Contender is already selected");
        }

        var contender = _waitingContenders.Dequeue();
        _emittedContenders.Add(contender.Key, contender.Value);
        return contender.Key;
    }

    public bool IsContenderEmitted(Contender contender)
    {
        return _emittedContenders.ContainsKey(contender);
    }

    public int SelectContender(Contender contender)
    {
        _contenderSelected = true;
        return _emittedContenders[contender];
    }

    public bool IsEmpty()
    {
        return _waitingContenders.Count == 0;
    }

    public int GetHallSize()
    {
        return _waitingContenders.Count;
    }
}