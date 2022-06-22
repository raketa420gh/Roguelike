using System.Threading;
using System.Threading.Tasks;

public interface IFirearms
{
    Task StartShooting(ITargetable target, CancellationToken cancellationToken);
    void StopShooting(CancellationTokenSource cancellationTokenSource);
}