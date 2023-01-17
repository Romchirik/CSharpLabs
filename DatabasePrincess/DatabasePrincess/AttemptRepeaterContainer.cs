using DatabasePrincess.Domain.Friend;
using DatabasePrincess.Domain.Hall;
using DatabasePrincess.Domain.Princess;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DatabasePrincess;

public class AttemptRepeaterContainer : BackgroundService
{
    private readonly ILogger<Princess> _logger;
    private readonly IHostApplicationLifetime _appLifetime;
    private readonly IHall _hall;
    private readonly Princess _princess;

    public AttemptRepeaterContainer(
        ILogger<Princess> logger,
        IHostApplicationLifetime appLifetime,
        IHall hall,
        IFriend friend
    )
    {
        _logger = logger;
        _appLifetime = appLifetime;
        _hall = hall;
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
    }
}