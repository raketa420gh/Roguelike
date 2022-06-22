using Zenject;

public class UnitsCounterInstaller : MonoInstaller
{
    public UnitsCounter unitsCounter;
    
    public override void InstallBindings() =>
        Bind();

    private void Bind()
    {
        Container
            .Bind<IUnitsCounter>()
            .FromInstance(unitsCounter)
            .AsSingle()
            .NonLazy();
    }
}