using Zenject;

public class EnemyDetectorInstaller : MonoInstaller
{
    public EnemyDetector EnemyDetector;
    
    public override void InstallBindings() =>
        BindEnemyDetector();

    private void BindEnemyDetector()
    {
        Container
            .Bind<IEnemyDetector>()
            .FromInstance(EnemyDetector)
            .AsSingle()
            .NonLazy();
    }
}