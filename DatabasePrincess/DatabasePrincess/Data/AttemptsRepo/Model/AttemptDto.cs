using System.ComponentModel.DataAnnotations;

namespace DatabasePrincess.Data.AttemptsRepo.Model;

public class AttemptDto
{
    [Key] public int AttemptId { get; set; }
    public int? LuckyOneIdx = null;
    public List<ContenderDto> Contenders { get; set; } = new();
}