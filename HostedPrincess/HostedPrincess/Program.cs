using HostedPrincess.Data.ContenderGenerator;
using HostedPrincess.Data.Friend;
using HostedPrincess.Data.Hall;
using HostedPrincess.Domain.ContenderGenerator;
using HostedPrincess.Domain.Friend;
using HostedPrincess.Domain.Hall;
using HostedPrincess.Domain.Princess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HostedPrincess;

public static class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<PrincessContainer>();
                services.AddScoped<Princess>();
                services.AddSingleton<IHall, HallImpl>();
                services.AddScoped<IFriend, FriendImpl>();
                services.AddSingleton<IContenderGenerator>(new LazyContenderGenerator(100));
            });
    }
}