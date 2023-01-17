using DatabasePrincess.Domain.ContenderGenerator;
using DatabasePrincess.Domain.Hall;

namespace DatabasePrincess.Data.Hall;

public class HallImpl : IHall
{
    private readonly Queue<KeyValuePair<Contender, int>> _waitingContenders;
    private readonly Dictionary<Contender, int> _emittedContenders = new();
    private readonly List<KeyValuePair<Contender, int>> _contenderHistory;
    private bool _contenderSelected;

    public HallImpl(IContenderGenerator generator)
    {
        _contenderHistory = generator.CreateApplicantsWithRating();
        _waitingContenders = new Queue<KeyValuePair<Contender, int>>(_contenderHistory);
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

    public List<KeyValuePair<Contender, int>> GetContendersHistory()
    {
        return _contenderHistory;
    }
}