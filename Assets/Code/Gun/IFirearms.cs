using System.Threading.Tasks;

public interface IFirearms
{
    Task StartShooting(ITargetable target);
    void StopShooting();
}