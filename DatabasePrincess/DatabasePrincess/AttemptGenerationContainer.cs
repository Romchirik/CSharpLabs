using DatabasePrincess.Domain.Attempts;
using DatabasePrincess.Domain.Friend;
using DatabasePrincess.Domain.Hall;
using DatabasePrincess.Domain.Model;
using DatabasePrincess.Domain.Princess;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DatabasePrincess;

public class AttemptGenerationContainer : BackgroundService
{
    private readonly ILogger<Princess> _logger;
    private readonly IHostApplicationLifetime _appLifetime;
    private readonly IHall _hall;
    private readonly IAttemptsRepo _attemptsRepo;
    private readonly AttemptId _attemptId;
    private readonly Princess _princess;

    public AttemptGenerationContainer(
        ILogger<Princess> logger,
        IHostApplicationLifetime appLifetime,
        IHall hall,
        IFriend friend,
        IAttemptsRepo attemptsRepo,
        AttemptId attemptId
    )
    {
        _logger = logger;
        _appLifetime = appLifetime;
        _hall = hall;
        _attemptsRepo = attemptsRepo;
        _attemptId = attemptId;
        _princess = new Princess(hall, friend, logger);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Run(Execute, stoppingToken);
        _appLifetime.StopApplication();
    }

    private void Execute()
    {
        var result = _princess.SelectContender();
        int rating;
        if (result != null)
        {
            rating = _hall.SelectContender(result);
            _logger.LogInformation("Selected contender {0} , {1} points", result, rating);
        }
        else
        {
            rating = 10;
            _logger.LogInformation("Contender not selected, {0} points", rating);
        }

        SaveAttempt(result, rating);
    }

    private void SaveAttempt(Contender? result, int rating)
    {
        var attempt = new Attempt
        {
            AttemptId = _attemptId.Id,
            LuckyOne = result != null ? new StorageContender(result.Id, result.Name, result.Surname, rating) : null,
            Contenders = _hall.GetContendersHistory().ConvertAll(pair =>
                new StorageContender(pair.Key.Id, pair.Key.Name, pair.Key.Surname, pair.Value)
            )
        };

        _attemptsRepo.SaveAttempt(attempt);
        _logger.LogInformation("Attempt saved: id {0}", _attemptId);
    }
}