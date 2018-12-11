
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

public class TimerService : IHostedService
{
    private Timer _timer;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}