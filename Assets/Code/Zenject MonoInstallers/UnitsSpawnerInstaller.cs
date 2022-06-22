using Zenject;

public class UnitsSpawnerInstaller : MonoInstaller
{
    public UnitsSpawner UnitsSpawner;

    public override void InstallBindings() =>
        Bind();

    private void Bind()
    {
        Container
            .Bind<IUnitsSpawner>()
            .FromInstance(UnitsSpawner)
            .AsSingle()
            .NonLazy();
    }
}