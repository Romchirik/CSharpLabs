using HostedPrincess.Domain.Hall;

namespace HostedPrincessTests;

using HostedPrincess.Data.ContenderGenerator;

public class ContenderGeneratorTest
{
    /// <summary>
    /// May flick because generator is based on random
    /// </summary>
    [Test]
    public void TestContenderNames()
    {
        for (int i = 0; i < 100; i++)
        {
            var generator = new LazyContenderGenerator(100);
            var set = new HashSet<Contender>(generator.CreateApplicantsWithRating().Select((pair => pair.Key)));
            Assert.That(set.Count, Is.EqualTo(100));
        }
    }
}