using DatabasePrincess.Data.AttemptsRepo.Model;
using DatabasePrincess.Domain.Attempts;

namespace DatabasePrincess.Data.AttemptsRepo.Mappers;

public static class ContenderMapper
{
    public static ContenderDto ToData(StorageContender model)
    {
        ContenderDto contender = new()
        {
            Id = model.Id.ToString(),
            Surname = model.Surname,
            Name = model.Name,
            rating = model.rating
        };

        return contender;
    }

    public static StorageContender ToDomain(ContenderDto model)
    {
        return new StorageContender(Guid.Parse(model.Id), model.Name, model.Surname, model.rating);
    }
}