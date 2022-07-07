using Zenject;

public class InveronmentalSpawnerInstaller : MonoInstaller
{
    public InveronmentalSpawner InveronmentalSpawner;

    public override void InstallBindings() =>
        Bind();

    private void Bind()
    {
        Container
            .Bind<IInveronmentalSpawner>()
            .FromInstance(InveronmentalSpawner)
            .AsSingle()
            .NonLazy();
    }
}