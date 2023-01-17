using CommandLine;
using CommandLine.Text;
using DatabasePrincess.Data.AttemptsRepo;
using DatabasePrincess.Data.ContenderGenerator;
using DatabasePrincess.Data.Friend;
using DatabasePrincess.Data.Hall;
using DatabasePrincess.Domain.Attempts;
using DatabasePrincess.Domain.ContenderGenerator;
using DatabasePrincess.Domain.Friend;
using DatabasePrincess.Domain.Hall;
using DatabasePrincess.Domain.Model;
using DatabasePrincess.Domain.Princess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DatabasePrincess;

public static class Program
{
    internal class Options
    {
        [Option('r', "restore", Required = false, HelpText = "Restores attempt with specified id")]
        public int? Id { get; set; }

        [Option('g', "generate", Required = false, HelpText = "Generates specified number of addempts")]
        public int? AttemptsNumber { get; set; }

        [Option("clear", Required = false, HelpText = "Clears attempts storage")]
        public bool Clear { get; set; }
    }


    public static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(opts => OnParseSuccess(opts, args));
    }

    private static void OnParseSuccess(Options o, string[] args)
    {
        if (o.Clear)
        {
            CreateStorageCleaner(args).Build().Run();
        }
        else if (o.Id != null)
        {
            CreateAttemptRepeater(args, (int)o.Id).Build().Run();
        }
        else if (o.AttemptsNumber != null)
        {
            foreach (var i in Enumerable.Range(1, (int)(o.AttemptsNumber + 1)))
            {
                CreateAttemptGenerator(args, i).Build().Run();
            }
        }
    }

    private static IHostBuilder CreateStorageCleaner(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<ClearContainer>();
                services.AddScoped<IAttemptsRepo, AttemptsRepoImpl>();
                services.AddDbContext<AttemptsContext>();
            });
    }

    private static IHostBuilder CreateAttemptGenerator(string[] args, int attemptId)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<AttemptGenerationContainer>();
                services.AddScoped<Princess>();
                services.AddSingleton<IHall, HallImpl>();
                services.AddScoped<IFriend, FriendImpl>();
                services.AddSingleton(new AttemptId(attemptId));
                services.AddScoped<IAttemptsRepo, AttemptsRepoImpl>();
                services.AddSingleton<IContenderGenerator>(new LazyContenderGenerator(100));
                services.AddDbContext<AttemptsContext>();
            });
    }

    private static IHostBuilder CreateAttemptRepeater(string[] args, int attemptId)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<AttemptRepeaterContainer>();
                services.AddScoped<Princess>();
                services.AddSingleton<IHall, HallImpl>();
                services.AddScoped<IFriend, FriendImpl>();
                services.AddScoped<IAttemptsRepo, AttemptsRepoImpl>();
                services.AddSingleton(new AttemptId(attemptId));
                services.AddSingleton<IContenderGenerator, AttemptContenderGenerator>();
                services.AddDbContext<AttemptsContext>();
            });
    }
}