using DatabasePrincess.Data.AttemptsRepo.Mappers;
using DatabasePrincess.Domain.Attempts;
using Microsoft.EntityFrameworkCore;

namespace DatabasePrincess.Data.AttemptsRepo;

public class AttemptsRepoImpl : IAttemptsRepo
{
    private readonly AttemptsContext _context;

    public AttemptsRepoImpl(AttemptsContext context)
    {
        _context = context;
    }

    public void SaveAttempt(Attempt attempt)
    {
        _context.Attempts.Add(AttemptMapper.ToData(attempt));
        _context.SaveChanges();
    }

    public void RemoveAttempt(int attemptId)
    {
        var target = _context.Attempts.Include(c => c.Contenders)
            .FirstOrDefault(attempt => attemptId == attempt.AttemptId);

        if (target != null)
        {
            _context.Remove(target);
            _context.SaveChanges();
        }
    }

    public Attempt? GetAttempt(int attemptId)
    {
        var target = _context.Attempts.Include(a => a.Contenders)
            .FirstOrDefault(attempt => attemptId == attempt.AttemptId);
        if (target != null)
        {
            return AttemptMapper.ToDomain(target);
        }

        return null;
    }

    public void RemoveAllAttempts()
    {
        _context.Attempts.RemoveRange(_context.Attempts);
        _context.SaveChanges();
    }
}