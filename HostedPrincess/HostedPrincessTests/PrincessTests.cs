using HostedPrincess.Data.Friend;
using HostedPrincess.Data.Hall;
using HostedPrincess.Domain.ContenderGenerator;
using HostedPrincess.Domain.Friend;
using HostedPrincess.Domain.Hall;
using HostedPrincess.Domain.Princess;
using Moq;

namespace HostedPrincessTests;

public class PrincessTests
{
    [Test]
    public void StrategyTestContendersStraightSorted()
    {
        var hallMock = new Mock<IHall>();
        var friendMock = new Mock<IFriend>();
        var princess = new Princess(hallMock.Object, friendMock.Object);

        friendMock.Setup(fm => fm.CompareContenders(It.IsAny<Contender>(), It.IsAny<Contender>()));
        var result = princess.SelectContender();
        Assert.That(result, Is.Null);
    }

    [Test]
    public void StrategyTestContendersReversedSorted()
    {
        var hallMock = new Mock<IHall>();
        var friendMock = new Mock<IFriend>();

        hallMock.SetupSequence(hm => hm.GetNextContender())
            .Returns(_contenders[10].Key)
            .Returns(_contenders[9].Key)
            .Returns(_contenders[8].Key)
            .Returns(_contenders[7].Key)
            .Returns(_contenders[6].Key)
            .Returns(_contenders[5].Key)
            .Returns(_contenders[4].Key)
            .Returns(_contenders[3].Key)
            .Returns(_contenders[2].Key)
            .Returns(_contenders[1].Key)
            .Returns(_contenders[0].Key);
        var princess = new Princess(hallMock.Object, friendMock.Object);

        friendMock.Setup(fm => fm.CompareContenders(It.IsAny<Contender>(), It.IsAny<Contender>()))
            .Returns((Contender c1, Contender c2) =>
            {
                var score1 = _contenders.Find(target => c1.Equals(target.Key)).Value;
                var score2 = _contenders.Find(target => c2.Equals(target.Key)).Value;

                if (score1 > score2)
                {
                    return c1;
                }

                if (score1 < score2)
                {
                    return c2;
                }

                return null;
            });

        var result = princess.SelectContender();
        Assert.That(result, Is.EqualTo(new Contender("Убытков", "Игорь")));
    }

    private readonly List<KeyValuePair<Contender, int>> _contenders = new()
    {
        new KeyValuePair<Contender, int>(new Contender("Пупкин", "Василий"), 100),
        new KeyValuePair<Contender, int>(new Contender("Иванов", "Павел"), 99),
        new KeyValuePair<Contender, int>(new Contender("Машинов", "Машина"), 98),
        new KeyValuePair<Contender, int>(new Contender("Убытков", "Ущерб"), 97),
        new KeyValuePair<Contender, int>(new Contender("Убытков", "Игорь"), 96),
        new KeyValuePair<Contender, int>(new Contender("Хагги", "Вагги"), 95),
        new KeyValuePair<Contender, int>(new Contender("Неверов", "Андрей"), 94),
        new KeyValuePair<Contender, int>(new Contender("Негодяев", "Ущерб"), 93),
        new KeyValuePair<Contender, int>(new Contender("Героев", "Ущерб"), 92),
        new KeyValuePair<Contender, int>(new Contender("Убытков", "Иннокентий"), 91),
        new KeyValuePair<Contender, int>(new Contender("Величаев", "Папич"), 90)
    };
}