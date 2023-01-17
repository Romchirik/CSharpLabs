using DatabasePrincess.Domain.Attempts;
using DatabasePrincess.Domain.Friend;
using DatabasePrincess.Domain.Hall;
using DatabasePrincess.Domain.Princess;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DatabasePrincess;

public class ClearContainer : BackgroundService
{
    private readonly ILogger<Princess> _logger;
    private readonly IHostApplicationLifetime _appLifetime;
    private readonly IAttemptsRepo _attemptsRepo;

    public ClearContainer(
        ILogger<Princess> logger,
        IHostApplicationLifetime appLifetime,
        IAttemptsRepo attemptsRepo
    )
    {
        _logger = logger;
        _appLifetime = appLifetime;
        _attemptsRepo = attemptsRepo;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Run(Execute, stoppingToken);
        _appLifetime.StopApplication();
    }

    private void Execute()
    {
        _attemptsRepo.RemoveAllAttempts();
    }
}