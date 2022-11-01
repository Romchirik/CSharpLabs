using HostedPrincess.Domain.Friend;
using HostedPrincess.Domain.Hall;
using HostedPrincess.Domain.Princess;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HostedPrincess;

public class PrincessContainer : BackgroundService
{
    private readonly ILogger<Princess> _logger;
    private readonly IHostApplicationLifetime _appLifetime;
    private readonly IHall _hall;
    private readonly Princess _princess;

    public PrincessContainer(
        ILogger<Princess> logger,
        IHostApplicationLifetime appLifetime,
        IHall hall,
        IFriend friend
    )
    {
        _logger = logger;
        _appLifetime = appLifetime;
        _hall = hall;
        _princess = new Princess(hall, friend);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Run(Execute, stoppingToken);
        _appLifetime.StopApplication();
    }

    private void Execute()
    {
        var result = _princess.SelectContender();
        if (result != null)
        {
            var rating = _hall.SelectContender(result);
            _logger.LogInformation("Selected contender ${0} , {1} points", result, rating);
        }
        else
        {
            _logger.LogInformation("Contender not selected, 10 points");
        }
    }
}