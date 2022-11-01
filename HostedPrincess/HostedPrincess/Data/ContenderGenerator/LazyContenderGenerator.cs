using HostedPrincess.Domain.ContenderGenerator;
using HostedPrincess.Domain.Hall;
using Strategy.Domain.Hall;

namespace HostedPrincess.Data.ContenderGenerator;

public class LazyContenderGenerator : IContenderGenerator
{
    private List<KeyValuePair<Contender, int>>? _contenders;
    private readonly int _poolSize;

    public LazyContenderGenerator(int poolSize)
    {
        _poolSize = poolSize;
    }

    public List<KeyValuePair<Contender, int>> CreateApplicantsWithRating()
    {
        if (_contenders == null)
        {
            var applicants = RandomApplicants(_poolSize);
            var ratings = Enumerable.Range(1, _poolSize).OrderBy(_ => Guid.NewGuid());
            _contenders = applicants.Zip(ratings,
                (applicant, rating) => new KeyValuePair<Contender, int>(applicant, rating)).ToList();
        }

        return _contenders;
    }


    private List<Contender> RandomApplicants(int poolSize)
    {
        bool success = false;
        var applicants = new HashSet<Contender>();
        while (!success)
        {
            var surnamesTmp = new List<string>();
            var namesTmp = new List<string>();

            for (int i = 0; i < poolSize; i++)
            {
                surnamesTmp.Add(Surnames.SurnamesList[i % Surnames.SurnamesList.Length]);
                namesTmp.Add(Names.NamesList[i % Names.NamesList.Length]);
            }

            var shuffledSurnames = surnamesTmp.OrderBy(_ => Guid.NewGuid()).ToList();
            var shuffledNames = namesTmp.OrderBy(_ => Guid.NewGuid()).ToList();

            foreach (var applicant in shuffledSurnames.Zip(shuffledNames,
                         (surname, name) => new Contender(name, surname)))
            {
                applicants.Add(applicant);
            }

            if (applicants.Count == poolSize)
            {
                success = true;
            }
            else
            {
                applicants.Clear();
            }
        }

        return applicants.ToList();
    }
}