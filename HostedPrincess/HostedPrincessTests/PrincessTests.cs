using HostedPrincess.Data.Friend;
using HostedPrincess.Data.Hall;
using HostedPrincess.Domain.ContenderGenerator;
using HostedPrincess.Domain.Hall;
using HostedPrincess.Domain.Princess;

namespace HostedPrincessTests;

public class PrincessTests
{
    [Test]
    public void PrincessStraightStrategyTest()
    {
        var generator = new StraightGenerator();
        var hall = new HallImpl(generator);
        var friend = new FriendImpl(generator, hall);

        var princess = new Princess(hall, friend);

        var result = princess.SelectContender();
        Assert.That(result, Is.Null);
    }

    [Test]
    public void PrincessReversedStrategyTest()
    {
        var generator = new ReversedGenerator();
        var hall = new HallImpl(generator);
        var friend = new FriendImpl(generator, hall);

        var princess = new Princess(hall, friend);

        var result = princess.SelectContender();
        Assert.That(result, Is.EqualTo(new Contender("Убытков", "Игорь")));
    }

    class StraightGenerator : IContenderGenerator
    {
        private readonly List<KeyValuePair<Contender, int>> _contenders = new();

        public StraightGenerator()
        {
            _contenders.Add(new KeyValuePair<Contender, int>(new Contender("Пупкин", "Василий"), 100));
            _contenders.Add(new KeyValuePair<Contender, int>(new Contender("Иванов", "Павел"), 99));
            _contenders.Add(new KeyValuePair<Contender, int>(new Contender("Машинов", "Машина"), 98));
            _contenders.Add(new KeyValuePair<Contender, int>(new Contender("Убытков", "Ущерб"), 97));
            _contenders.Add(new KeyValuePair<Contender, int>(new Contender("Убытков", "Игорь"), 96));
            _contenders.Add(new KeyValuePair<Contender, int>(new Contender("Хагги", "Вагги"), 95));
            _contenders.Add(new KeyValuePair<Contender, int>(new Contender("Неверов", "Андрей"), 94));
            _contenders.Add(new KeyValuePair<Contender, int>(new Contender("Негодяев", "Ущерб"), 93));
            _contenders.Add(new KeyValuePair<Contender, int>(new Contender("Героев", "Ущерб"), 92));
            _contenders.Add(new KeyValuePair<Contender, int>(new Contender("Убытков", "Иннокентий"), 91));
            _contenders.Add(new KeyValuePair<Contender, int>(new Contender("Величаев", "Папич"), 90));
        }

        public List<KeyValuePair<Contender, int>> CreateApplicantsWithRating()
        {
            return _contenders;
        }
    }

    class ReversedGenerator : IContenderGenerator
    {
        private readonly List<KeyValuePair<Contender, int>> _contenders = new();

        public ReversedGenerator()
        {
            _contenders.Add(new KeyValuePair<Contender, int>(new Contender("Величаев", "Папич"), 90));
            _contenders.Add(new KeyValuePair<Contender, int>(new Contender("Убытков", "Иннокентий"), 91));
            _contenders.Add(new KeyValuePair<Contender, int>(new Contender("Героев", "Ущерб"), 92));
            _contenders.Add(new KeyValuePair<Contender, int>(new Contender("Негодяев", "Ущерб"), 93));
            _contenders.Add(new KeyValuePair<Contender, int>(new Contender("Неверов", "Андрей"), 94));
            _contenders.Add(new KeyValuePair<Contender, int>(new Contender("Хагги", "Вагги"), 95));
            _contenders.Add(new KeyValuePair<Contender, int>(new Contender("Убытков", "Игорь"), 96));
            _contenders.Add(new KeyValuePair<Contender, int>(new Contender("Убытков", "Ущерб"), 97));
            _contenders.Add(new KeyValuePair<Contender, int>(new Contender("Машинов", "Машина"), 98));
            _contenders.Add(new KeyValuePair<Contender, int>(new Contender("Иванов", "Павел"), 99));
            _contenders.Add(new KeyValuePair<Contender, int>(new Contender("Пупкин", "Василий"), 100));
        }

        public List<KeyValuePair<Contender, int>> CreateApplicantsWithRating()
        {
            return _contenders;
        }
    }
}