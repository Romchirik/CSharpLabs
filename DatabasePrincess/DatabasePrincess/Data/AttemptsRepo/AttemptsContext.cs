using DatabasePrincess.Data.AttemptsRepo.Model;
using Microsoft.EntityFrameworkCore;

namespace DatabasePrincess.Data.AttemptsRepo;

public class AttemptsContext : DbContext
{
    public DbSet<AttemptDto> Attempts { get; set; }

    public string DbPath { get; }


    public AttemptsContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "attempts.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}