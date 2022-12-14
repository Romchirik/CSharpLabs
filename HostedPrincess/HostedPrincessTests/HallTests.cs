using HostedPrincess.Data.ContenderGenerator;
using HostedPrincess.Data.Hall;
using HostedPrincess.Domain.ContenderGenerator;
using HostedPrincess.Domain.Hall;
using Moq;

namespace HostedPrincessTests;

public class HallTests
{
    [Test]
    public void CallNextContenderTest()
    {
        var actualContenders = new List<Contender>();
        var testGenerator = Mock.Of<IContenderGenerator>(ld => ld.CreateApplicantsWithRating() == _contenders);
        var hall = new HallImpl(testGenerator);

        for (int i = 0; i < _contenders.Count; i++)
        {
            actualContenders.Add(hall.GetNextContender());
        }

        foreach (var pair in _contenders)
        {
            Assert.That(actualContenders.Contains(pair.Key), Is.True);
        }
    }

    [Test]
    public void ThrowOnEmptyTest()
    {
        var testGenerator = Mock.Of<IContenderGenerator>(ld => ld.CreateApplicantsWithRating() == _contenders);
        var hall = new HallImpl(testGenerator);

        for (int i = 0; i < _contenders.Count; i++)
        {
            hall.GetNextContender();
        }

        Assert.That(hall.GetNextContender, Throws.InvalidOperationException);
    }

    private readonly List<KeyValuePair<Contender, int>> _contenders = new()
    {
        new KeyValuePair<Contender, int>(new Contender("Пупкин", "Василий"), 100),
        new KeyValuePair<Contender, int>(new Contender("Иванов", "Павел"), 99),
        new KeyValuePair<Contender, int>(new Contender("Машинов", "Машина"), 98),
        new KeyValuePair<Contender, int>(new Contender("Убытков", "Ущерб"), 97),
    };
}