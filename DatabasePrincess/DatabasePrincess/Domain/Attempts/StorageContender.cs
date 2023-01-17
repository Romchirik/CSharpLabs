namespace DatabasePrincess.Domain.Attempts;

public class StorageContender
{
    public StorageContender(Guid id, string name, string surname, int rating)
    {
        Id = id;
        Name = name;
        Surname = surname;
        this.rating = rating;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int rating { get; set; }
}