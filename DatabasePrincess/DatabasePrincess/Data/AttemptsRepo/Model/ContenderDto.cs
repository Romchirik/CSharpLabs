using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DatabasePrincess.Data.AttemptsRepo.Model;

public class ContenderDto
{
    [Key] public string Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

    public int rating { get; set; }

    public int AttemptId { get; set; }

    public int Order { get; set; }
    public AttemptDto AttemptDto { get; set; }
}