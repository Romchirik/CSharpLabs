using DatabasePrincess.Domain.Hall;

namespace DatabasePrincess.Domain.Attempts;

public class Attempt
{
    public int AttemptId { get; set; }
    public List<StorageContender> Contenders { get; set; } = new();
    public StorageContender? LuckyOne { get; set; }
}