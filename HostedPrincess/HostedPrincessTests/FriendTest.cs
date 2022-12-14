using HostedPrincess.Data.Friend;
using HostedPrincess.Data.Hall;
using HostedPrincess.Domain.ContenderGenerator;
using HostedPrincess.Domain.Hall;
using Moq;

namespace HostedPrincessTests;

public class FriendTest
{
    [Test]
    public void TestComparison()
    {
        var testGenerator = Mock.Of<IContenderGenerator>(ld => ld.CreateApplicantsWithRating() == _contenders);
        var hallMock = new Mock<IHall>();
        var friend = new FriendImpl(testGenerator, hallMock.Object);

        var contender0 = _contenders[0];
        var contender1 = _contenders[1];

        hallMock.Setup(hm => hm.IsContenderEmitted(contender0.Key)).Returns(true);
        hallMock.Setup(hm => hm.IsContenderEmitted(contender1.Key)).Returns(true);

        Assert.That(friend.CompareContenders(contender0.Key, contender1.Key), Is.EqualTo(contender0.Key));
        Assert.That(friend.CompareContenders(contender1.Key, contender0.Key), Is.EqualTo(contender0.Key));
        Assert.That(friend.CompareContenders(contender0.Key, contender0.Key), Is.Null);
    }

    [Test]
    public void TestContenderConsistencyCheck()
    {
        var generatorMock = Mock.Of<IContenderGenerator>(ld => ld.CreateApplicantsWithRating() == _contenders);
        var hallMock = new Mock<IHall>();

        var contender0 = _contenders[0];
        var contender1 = _contenders[1];
        var friend = new FriendImpl(generatorMock, hallMock.Object);

        hallMock.Setup(hm => hm.IsContenderEmitted(contender0.Key)).Returns(false);
        hallMock.Setup(hm => hm.IsContenderEmitted(contender1.Key)).Returns(false);

        Assert.That(() => friend.CompareContenders(contender0.Key, contender1.Key), Throws.InvalidOperationException);

        hallMock.Setup(hm => hm.IsContenderEmitted(contender0.Key)).Returns(true);
        hallMock.Setup(hm => hm.IsContenderEmitted(contender1.Key)).Returns(true);

        Assert.That(() => friend.CompareContenders(contender0.Key, contender1.Key), Throws.Nothing);
    }

    private readonly List<KeyValuePair<Contender, int>> _contenders = new()
    {
        KeyValuePair.Create(new Contender("Пупкин", "Василий"), 100),
        KeyValuePair.Create(new Contender("Иванов", "Иван"), 99),
        KeyValuePair.Create(new Contender("Машинов", "Машина"), 98),
        KeyValuePair.Create(new Contender("Убытков", "Ущерб"), 97),
    };
}