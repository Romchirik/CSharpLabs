using HostedPrincess.Data.ContenderGenerator;
using HostedPrincess.Data.Hall;
using HostedPrincess.Domain.ContenderGenerator;
using HostedPrincess.Domain.Hall;

namespace HostedPrincessTests;

public class HallTests
{
    [Test]
    public void CallNextContenderTest()
    {
        var actualContenders = new List<Contender>();
        var testGenerator = new TestGenerator();
        var hall = new HallImpl(testGenerator);

        for (int i = 0; i < testGenerator.Contenders.Count; i++)
        {
            actualContenders.Add(hall.GetNextContender());
        }

        foreach (var pair in testGenerator.Contenders)
        {
            Assert.That(actualContenders.Contains(pair.Key), Is.True);
        }
    }

    [Test]
    public void ThrowOnEmptyTest()
    {
        var testGenerator = new TestGenerator();
        var hall = new HallImpl(testGenerator);

        for (int i = 0; i < testGenerator.Contenders.Count; i++)
        {
            hall.GetNextContender();
        }

        Assert.That(hall.GetNextContender, Throws.InvalidOperationException);
    }

    class TestGenerator : IContenderGenerator
    {
        public readonly List<KeyValuePair<Contender, int>> Contenders = new();

        public TestGenerator()
        {
            Contenders.Add(new KeyValuePair<Contender, int>(new Contender("Пупкин", "Василий"), 100));
            Contenders.Add(new KeyValuePair<Contender, int>(new Contender("Иванов", "Павел"), 99));
            Contenders.Add(new KeyValuePair<Contender, int>(new Contender("Машинов", "Машина"), 98));
            Contenders.Add(new KeyValuePair<Contender, int>(new Contender("Убытков", "Ущерб"), 97));
        }

        public List<KeyValuePair<Contender, int>> CreateApplicantsWithRating()
        {
            return Contenders;
        }
    }
}