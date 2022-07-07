using System.Threading;
using System.Threading.Tasks;

public interface IFirearms
{
    void Setup(float shootingSpeed);
    Task StartShooting(ITargetable target, CancellationToken cancellationToken);
    void StopShooting(CancellationTokenSource cancellationTokenSource);
}