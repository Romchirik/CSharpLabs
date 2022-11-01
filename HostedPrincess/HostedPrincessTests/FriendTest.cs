using HostedPrincess.Data.Friend;
using HostedPrincess.Data.Hall;
using HostedPrincess.Domain.ContenderGenerator;
using HostedPrincess.Domain.Hall;

namespace HostedPrincessTests;

public class FriendTest
{
    [Test]
    public void TestComparison()
    {
        var testGenerator = new TestGenerator();
        var hall = new HallImpl(testGenerator);
        var friend = new FriendImpl(testGenerator, hall);

        var contender1 = new Contender("Пупкин", "Василий");
        var contender2 = new Contender("Иванов", "Иван");

        hall.GetNextContender();
        hall.GetNextContender();

        Assert.That(friend.CompareContenders(contender1, contender2), Is.EqualTo(contender1));
        Assert.That(friend.CompareContenders(contender2, contender1), Is.EqualTo(contender1));
        Assert.That(friend.CompareContenders(contender1, contender1), Is.Null);
    }

    [Test]
    public void TestNotAcquainted()
    {
        var testGenerator = new TestGenerator();
        var hall = new HallImpl(testGenerator);
        var friend = new FriendImpl(testGenerator, hall);

        var contender1 = new Contender("Пупкин", "Василий");
        var contender2 = new Contender("Иванов", "Иван");

        Assert.That(() => friend.CompareContenders(contender1, contender2), Throws.InvalidOperationException);

        hall.GetNextContender();
        hall.GetNextContender();

        Assert.That(() => friend.CompareContenders(contender1, contender2), Throws.Nothing);
    }

    class TestGenerator : IContenderGenerator
    {
        private readonly List<KeyValuePair<Contender, int>> _contenders = new();

        public TestGenerator()
        {
            _contenders.Add(new KeyValuePair<Contender, int>(new Contender("Пупкин", "Василий"), 100));
            _contenders.Add(new KeyValuePair<Contender, int>(new Contender("Иванов", "Иван"), 99));
            _contenders.Add(new KeyValuePair<Contender, int>(new Contender("Машинов", "Машина"), 98));
            _contenders.Add(new KeyValuePair<Contender, int>(new Contender("Убытков", "Ущерб"), 97));
        }

        public List<KeyValuePair<Contender, int>> CreateApplicantsWithRating()
        {
            return _contenders;
        }
    }
}