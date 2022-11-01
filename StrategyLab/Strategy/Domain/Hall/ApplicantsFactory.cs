namespace Strategy.Domain.Hall;

public class ApplicantsFactory
{
    public static Dictionary<Applicant, int> CreateApplicantsWithRating(int poolSize)
    {
        var applicants = RandomApplicants(poolSize);
        var ratings = Enumerable.Range(1, poolSize).OrderBy(_ => Guid.NewGuid());
        return new Dictionary<Applicant, int>(applicants.Zip(ratings,
            (applicant, rating) => new KeyValuePair<Applicant, int>(applicant, rating)));
    }

    public static List<Applicant> RandomApplicants(int poolSize)
    {
        bool success = false;
        var applicants = new HashSet<Applicant>();
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
                         (surname, name) => new Applicant(name, surname)))
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