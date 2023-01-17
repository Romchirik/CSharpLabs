using DatabasePrincess.Data.AttemptsRepo.Model;
using DatabasePrincess.Domain.Attempts;

namespace DatabasePrincess.Data.AttemptsRepo.Mappers;

public static class AttemptMapper
{
    public static AttemptDto ToData(Attempt model)
    {
        var tmp = model.Contenders.Select((item, index) =>
        {
            var cont = ContenderMapper.ToData(item);
            cont.Order = index;
            return cont;
        }).ToList();

        AttemptDto attemptDto = new()
        {
            AttemptId = model.AttemptId,
            Contenders = tmp,
            LuckyOneIdx = tmp.FindIndex(it => it.Name == model.LuckyOne?.Name && it.Surname == model.LuckyOne?.Surname)
        };
        attemptDto.Contenders.ForEach(dto => dto.AttemptDto = attemptDto);
        return attemptDto;
    }


    public static Attempt ToDomain(AttemptDto model)
    {
        var tmp = model.Contenders.OrderBy(it => it.Order).ToList().ConvertAll(ContenderMapper.ToDomain);
        Attempt attempt = new()
        {
            AttemptId = model.AttemptId,
            Contenders = tmp,
            LuckyOne = model.LuckyOneIdx == null ? null : tmp[(int)model.LuckyOneIdx]
        };
        return attempt;
    }
}