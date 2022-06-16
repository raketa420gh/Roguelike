using Zenject;

public class EnemyCounterInstaller : MonoInstaller
{
    public EnemyCounter EnemyCounter;
    
    public override void InstallBindings() =>
        BindEnemyCounter();

    private void BindEnemyCounter()
    {
        Container
            .Bind<IEnemyCounter>()
            .FromInstance(EnemyCounter)
            .AsSingle()
            .NonLazy();
    }
}