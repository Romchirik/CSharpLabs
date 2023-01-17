namespace DatabasePrincess.Domain.Hall;

public interface IHall
{
    public Contender GetNextContender();

    public bool IsContenderEmitted(Contender contender);

    public int SelectContender(Contender contender);

    public bool IsEmpty();

    public int GetHallSize();

    public List<KeyValuePair<Contender, int>> GetContendersHistory();
}