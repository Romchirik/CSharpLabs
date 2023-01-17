namespace DatabasePrincess.Domain.Attempts;

public interface IAttemptsRepo
{
    public void SaveAttempt(Attempt attempt);

    public void RemoveAttempt(int attemptId);

    public Attempt GetAttempt(int attemptId);

    public void RemoveAllAttempts();
}